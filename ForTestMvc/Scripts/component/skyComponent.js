

// namespace
window.skyComponent = window.skyComponent || {};

// 单选框
// dataUrl 数据获取路径
// paraObj request 参数（json 格式）
window.skyComponent.SelectCmp = function (dataUrl, reqPara) {
    // Fields
    this._config = {
        name: "单选框",
        title: "我的单选框",
        defaultValue: "-1",
    };
    this._url = dataUrl;
    this._reqPara = reqPara;
    this._optionItems = [];
    this._element = $(window.skyComponent.SelectCmp.html);
    var _this = this;

    // Methods 
    this.setConfig = function (configObj) {
        _this._config = this.configObj;
    }

    // 异步获取后端数据
    // action：后端的数据获取接口
    this.getJsonData = function () {
        if (_this._url != undefined && _this._url != null && _this._url != '') {
            var ajax = {
                url: _this._url,
                type: 'Post',
                dataType: 'json',
                cache: false,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.length > 0) {
                        // 赋值&处理
                        _this._optionItems = data;
                        _this.renderElement();
                        _this.setSelected("-1");
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
            var opt = '<li value=' + (i + 1) + '>' + _this._optionItems[i] + '</li>';
            _this._element.find('[gi~="lblOptions"]').append(opt);
        }
    }

    this.setSelected = function (value) {
        _this._config.defaultValue = value;
    }

    // 获取页面
    this.getElement = function () {
        return _this._element;
    }

    // 初始化函数
    this.initCmp = function () {
        //_this.setConfig(null);
        _this.getJsonData();
    }
    this.initCmp();

    // 动作事件
    _this._element.find('[gi~="lblOptions"]').bind("click", function (e) {
        //$(e.currentTarget).toggleClass("active");
        //$(".popup-ListDate").not($(this)).removeClass('active');
        alert(123);
    });

}

// 组件标签
window.skyComponent.SelectCmp.html =
    '<div>' +
        '<ol gi="lblOptions">' +
        '</ol>' +
    '</div>';

// 拓展方法
//window.skyComponent.extendClass(SelectCmp, )
