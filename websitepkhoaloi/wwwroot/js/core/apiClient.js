/**
 * API Client Template - Dùng chung cho tất cả các file cshtml
 * Hỗ trợ GET, POST, PUT, DELETE request
 */
class ApiClient {
    constructor(url, options = {}) {
        this.url = url;
        this.timeout = options.timeout || 30000;
        this.loadingSelector = options.loadingSelector || null;
        this.modalSelector = options.modalSelector || '#modal-add';
        this.formSelector = options.formSelector || '#userForm';
    }

    /**
     * Hiển thị loading
     */
    showLoading() {
        if (this.loadingSelector) {
            $(this.loadingSelector).show();
        }
    }

    /**
     * Ẩn loading
     */
    hideLoading() {
        if (this.loadingSelector) {
            $(this.loadingSelector).hide();
        }
    }

    /**
     * Xử lý response thành công
     */
    handleSuccess(response, options = {}) {
        const {
            hideModal = true,
            resetForm = true,
            callback = null
        } = options;

        if (response.success) {
            if (hideModal && this.modalSelector) {
                $(this.modalSelector).modal('hide');
            }
            if (resetForm && this.formSelector) {
                $(this.formSelector)[0].reset();
            }
            swal.fire("Thành công", response.message || "Lưu dữ liệu thành công", "success");
            if (callback) callback(response);
        } else {
            swal.fire("Thất bại", response.message || "Có lỗi xảy ra", "error");
        }
    }

    /**
     * Xử lý lỗi
     */
    handleError(error) {
        console.error('API Error:', error);
        swal.fire("Lỗi", error.statusText || "Có lỗi kết nối đến server", "error");
    }

    /**
     * POST Request - Lưu dữ liệu
     */
    post(formData, options = {}) {
        this.showLoading();
        const self = this;

        $.ajax({
            url: this.url,
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            timeout: this.timeout,
            success: function(response) {
                self.hideLoading();
                self.handleSuccess(response, options);
            },
            error: function(error) {
                self.hideLoading();
                self.handleError(error);
            }
        });
    }

    /**
     * GET Request - Lấy dữ liệu
     */
    get(params = {}, options = {}) {
        this.showLoading();
        const self = this;
        const queryString = new URLSearchParams(params).toString();
        const url = queryString ? `${this.url}?${queryString}` : this.url;

        $.ajax({
            url: url,
            type: 'GET',
            dataType: 'json',
            timeout: this.timeout,
            success: function(response) {
                self.hideLoading();
                if (options.callback) {
                    options.callback(response);
                }
            },
            error: function(error) {
                self.hideLoading();
                self.handleError(error);
            }
        });
    }

    /**
     * PUT Request - Cập nhật dữ liệu
     */
    put(formData, options = {}) {
        this.showLoading();
        const self = this;

        $.ajax({
            url: this.url,
            type: 'PUT',
            data: formData,
            processData: false,
            contentType: false,
            timeout: this.timeout,
            success: function(response) {
                self.hideLoading();
                self.handleSuccess(response, options);
            },
            error: function(error) {
                self.hideLoading();
                self.handleError(error);
            }
        });
    }

    /**
     * DELETE Request - Xóa dữ liệu
     */
    delete(params = {}, options = {}) {
        this.showLoading();
        const self = this;
        const queryString = new URLSearchParams(params).toString();
        const url = queryString ? `${this.url}?${queryString}` : this.url;

        $.ajax({
            url: url,
            type: 'DELETE',
            timeout: this.timeout,
            success: function(response) {
                self.hideLoading();
                self.handleSuccess(response, options);
            },
            error: function(error) {
                self.hideLoading();
                self.handleError(error);
            }
        });
    }

    /**
     * PATCH Request - Cập nhật một phần dữ liệu
     */
    patch(data, options = {}) {
        this.showLoading();
        const self = this;

        $.ajax({
            url: this.url,
            type: 'PATCH',
            data: JSON.stringify(data),
            dataType: 'json',
            contentType: 'application/json',
            timeout: this.timeout,
            success: function(response) {
                self.hideLoading();
                self.handleSuccess(response, options);
            },
            error: function(error) {
                self.hideLoading();
                self.handleError(error);
            }
        });
    }
}

// Backward compatibility - giữ tên cũ
class apicall extends ApiClient {}