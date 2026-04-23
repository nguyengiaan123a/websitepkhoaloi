/**
 * API Client Template - Dùng chung
 */
class ApiClient {
    constructor(url, options = {}) {
        this.url = url;
        this.timeout = options.timeout || 30000;
        this.loadingSelector = options.loadingSelector || null;
        this.modalSelector = options.modalSelector || '#modal-add';
        this.formSelector = options.formSelector || '#userForm';
    }

    // ===== LOADING =====
    showLoading() {
        if (this.loadingSelector) {
            $(this.loadingSelector).show();
        }
    }

    hideLoading() {
        if (this.loadingSelector) {
            $(this.loadingSelector).hide();
        }
    }

    // ===== SUCCESS HANDLER =====
    handleSuccess(response, options = {}) {
        const {
            hideModal = true,
            resetForm = true,
            showMessage = true,
            successMessage = null,
            errorMessage = null,
            callback = null
        } = options;

        if (response.success) {

            if (hideModal && this.modalSelector) {
                $(this.modalSelector).modal('hide');
            }

            if (resetForm && this.formSelector) {
                $(this.formSelector)[0].reset();
            }

            if (showMessage) {
                swal.fire(
                    "Thành công",
                    successMessage || response.message || "Lưu dữ liệu thành công",
                    "success"
                );
            }

        } else {
            if (showMessage) {
                swal.fire(
                    "Thất bại",
                    errorMessage || response.message || "Có lỗi xảy ra",
                    "error"
                );
            }
        }

        // Gọi callback luôn, dù thành công hay thất bại
        if (callback) callback(response);
    }

    // ===== ERROR HANDLER =====
    handleError(error, options = {}) {
        const {
            showMessage = true,
            errorMessage = null
        } = options;

        console.error('API Error:', error);

        if (showMessage) {
            swal.fire(
                "Lỗi",
                errorMessage || error.statusText || "Có lỗi kết nối đến server",
                "error"
            );
        }
    }

    // ===== POST =====
    post(formData, options = {}) {
        this.showLoading();

        $.ajax({
            url: this.url,
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            timeout: this.timeout,
            success: (response) => {
                this.hideLoading();
                this.handleSuccess(response, options);
            },
            error: (error) => {
                this.hideLoading();
                this.handleError(error, options);
            }
        });
    }

    // ===== GET =====
    get(params = {}, options = {}) {
        this.showLoading();

        const queryString = new URLSearchParams(params).toString();
        const url = queryString ? `${this.url}?${queryString}` : this.url;

        $.ajax({
            url: url,
            type: 'GET',
            dataType: 'json',
            timeout: this.timeout,
            success: (response) => {
                this.hideLoading();
                if (options.callback) {
                    options.callback(response);
                }
            },
            error: (error) => {
                this.hideLoading();
                this.handleError(error, options);
            }
        });
    }

    // ===== PUT =====
    put(formData, options = {}) {
        this.showLoading();

        $.ajax({
            url: this.url,
            type: 'PUT',
            data: formData,
            processData: false,
            contentType: false,
            timeout: this.timeout,
            success: (response) => {
                this.hideLoading();
                this.handleSuccess(response, options);
            },
            error: (error) => {
                this.hideLoading();
                this.handleError(error, options);
            }
        });
    }

    // ===== DELETE =====
    delete(params = {}, options = {}) {
        this.showLoading();

        const queryString = new URLSearchParams(params).toString();
        const url = queryString ? `${this.url}?${queryString}` : this.url;

        $.ajax({
            url: url,
            type: 'DELETE',
            timeout: this.timeout,
            success: (response) => {
                this.hideLoading();
                this.handleSuccess(response, options);
            },
            error: (error) => {
                this.hideLoading();
                this.handleError(error, options);
            }
        });
    }

    // ===== PATCH =====
    patch(data, options = {}) {
        this.showLoading();

        $.ajax({
            url: this.url,
            type: 'PATCH',
            data: JSON.stringify(data),
            dataType: 'json',
            contentType: 'application/json',
            timeout: this.timeout,
            success: (response) => {
                this.hideLoading();
                this.handleSuccess(response, options);
            },
            error: (error) => {
                this.hideLoading();
                this.handleError(error, options);
            }
        });
    }
}

// Backward compatibility
class apicall extends ApiClient {}