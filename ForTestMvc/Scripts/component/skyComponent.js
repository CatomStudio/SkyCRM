

// namespace
window.skyComponent = window.skyComponent || {};

// 单选框
// dataUrl 数据获取路径
// paraObj request 参数（json 格式）
window.skyComponent.SelectCmp = function (dataUrl, reqPara) {
    // Html
    this.html =
        '<div>' +
            '<select gi="lblOptions">' +
            '</select>' +
        '</div>';
    this._optionFormat = '<option class="" id="" value=""></option>';

    // Fields
    this._config = {
        name: "单选框",
        title: "我的单选框",
        defaultValue: "-1",
    };
    this._url = dataUrl;
    this._reqPara = reqPara;
    this._optionItems = [];
    this._return = null;
    var _this = this;
    var $scope = $(this.html);

    // Methods 
    this.setConfig = function (configObj) {
        _this._config = this.configObj;
    }

    // 1. 赋值&渲染节点
    // action：后端的数据获取接口
    this.getJsonData = function () {
        if (_this._url != undefined && _this._url != null && _this._url != '') {
            var ajax = {
                url: _this._url,
                async: false, // 防止后续函数的异步执行
                type: 'Post',
                dataType: 'json',
                cache: false,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.length > 0) {
                        // 赋值
                        _this._optionItems = data;
                    }
                },
                error: function () {
                    alert('系统失败，请联系管理员！', 3);
                }
            }
            if (_this._reqPara != undefined && _this._reqPara != null && _this._reqPara != '') {
                ajax.data = JSON.stringify(_this._reqPara);
            }
            $.ajax(ajax);
        }
    }

    this.renderElement = function () {
        for (var i = 0; i < _this._optionItems.length; i++) {
            var opt = $(_this._optionFormat);
            opt.val(i + 1);
            opt.html(_this._optionItems[i]);
            $scope.find('[gi~="lblOptions"]').append(opt);
        }
    }

    this.setSelected = function (value) {
        _this._config.defaultValue = value;
    }

    // 2. 获取页面
    this.getElement = function () {
        return $scope;
    }

    // 3. 初始化函数
    this.initCmp = function () {
        //_this.setConfig(null);
        _this.getJsonData();
        _this.renderElement();
        _this.setSelected("-1");
    }
    this.initCmp();

    // 4. 动作事件（赋值&取值）
    this.getValue = function () {
        return _this._return;
    }

    $scope.find('[gi~="lblOptions"]').bind("change", function (e) {
        //$(e.currentTarget).toggleClass("active");
        //$(".popup-ListDate").not($(this)).removeClass('active');
        _this._return = e.target.selectedOptions[0].value;
    });

    // 5. 回调函数
}

// 拓展方法
//window.skyComponent.extendClass(SelectCmp, funcs )




// namespace
window.skyComponent.TextPopupCmp = function () {
    // Html 
    this.html =
        '<div gi="lblTextPanel">' +
            '<input gi="lblText" />' +
            '<span gi="btnDelete" name="Delete">删除</span>' +
            '<button gi="btnCancel" name="Cancel">取消</button>' +
            '<button gi="btnConfirm" name="Confirm">确定</button>' +
        '</div>';

    // Fields
    var _this = this;
    var $scope = $(this.html);
    this._title = "文本输入框";
    this._text = "";
    this._confirm = $scope.find('[gi~="btnConfirm"]');
    this._cancel = $scope.find('[gi~="btnCancel"]');
    this._confirmCallBack = null;
    this._cancelCallBack = null;

    // private methods
    this.setText = function (text) {
        _this._text = text == undefined || text == null ? "" : text;
        $scope.find('[gi~="lblText"]').val(_this._text);
    }

    this.getText = function () {
        _this._text = $scope.find('[gi~="lblText"]').val();
        return _this._text;
    }

    this.getElement = function () {
        return $scope;
    }

    // public methods 添加回调函数
    this.addConfirmCallBack = function (confirmCB) {
        _this.confirmCallBack = confirmCB;
    }

    this.addCancelCallBack = function (cancelCB) {
        _this.cancelCallBack = cancelCB;
    }

    // 事件 & 赋值取值
    $scope.find('[gi~="btnConfirm"]').bind('click', function () {
        if (_this.confirmCallBack) {
            _this.confirmCallBack(_this.getText());
        }
    });

    $scope.find('[gi~="btnCancel"]').bind('click', function () {
        _this.setText("");
        if (_this.cancelCallBack) {
            _this.cancelCallBack();
        }
    });

    $scope.find('[gi~="btnDelete"]').bind('click', function () {
        $scope.remove();
    })

}








