String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}
String.prototype.ltrim = function () {
    return this.replace(/(^\s*)/g, "");
}
String.prototype.rtrim = function () {
    return this.replace(/(\s*$)/g, "");
}

//回车禁用
jQuery("body").bind("keydown", function (e) {
    if (e.which == 13) {
        return false;
    }
});

//去除字符串头尾空格或指定字符
String.prototype.Trim = function (c) {
    if (c == null || c == "") {
        var str = this.replace(/^\s*/, '');
        var rg = /\s/;
        var i = str.length;
        while (rg.test(str.charAt(--i)));
        return str.slice(0, i + 1);
    }
    else {
        var rg = new RegExp("^" + c + "*");
        var str = this.replace(rg, '');
        rg = new RegExp(c);
        var i = str.length;
        while (rg.test(str.charAt(--i)));
        return str.slice(0, i + 1);
    }
}

//去除字符串头部空格或指定字符
String.prototype.TrimStart = function (c) {
    if (c == null || c == "") {
        var str = this.replace(/^\s*/, '');
        return str;
    }
    else {
        var rg = new RegExp("^" + c + "*");
        var str = this.replace(rg, '');
        return str;
    }
}

//去除字符串尾部空格或指定字符
String.prototype.trimEnd = function (c) {
    if (c == null || c == "") {
        var str = this;
        var rg = /\s/;
        var i = str.length;
        while (rg.test(str.charAt(--i)));
        return str.slice(0, i + 1);
    }
    else {
        var str = this;
        var rg = new RegExp(c);
        var i = str.length;

        while (rg.test(str.charAt(--i)));
        return str.slice(0, i + 1);
    }
}

function stopPropagation(e) {
    e = e || window.event;
    if (e.stopPropagation) { //W3C阻止冒泡方法
        e.stopPropagation();
    } else {
        e.cancelBubble = true; //IE阻止冒泡方法
    }
}
//取得cookie
function getCookie(name) {
    var str = document.cookie.split(";")
    for (var i = 0; i < str.length; i++) {
        var str2 = str[i].split("=");
        if (str2[0].trim() == name)
            return unescape(str2[1]);
    }
}
function showProcess(isShow, title, msg) {
    if (!isShow) {
        $.messager.progress('close');
        return;
    }
    var win = $.messager.progress({
        title: title,
        msg: msg
    });
}
///秒数转换
function GetSecondsName(seconds) {
    seconds = parseInt(seconds);
    var str = "";
    if (seconds / 3600 > 0) {
        str += parseInt(seconds / 3600);
        str += "小时";
    }
    if ((seconds % 3600) / 60 > 0) {
        str += parseInt((seconds % 3600) / 60);
        str += "分钟";
    }
    if ((seconds % 3600) % 60 > 0) {
        str += (seconds % 3600) % 60;
        str += "秒";
    }
    return str;
}
function ShowLinkFormater(val, rowdata, index) {
    return '<a href="http://' + val + '" target="_blank">' + val + '</a>';
}

function ShowImgFormater(val, rowdata, index) {
    return '<img src="' + val + '" style="height:100px;width:100px;"/>';
}

//通过TeantId绑定方案
function LoadProgramByTeantId(teantId, programContorlId) {
    $("#" + programContorlId).empty();
    var TenantId = teantId;
    if (TenantId != '') {
        var htmlStr = "";
        $.post("/Admin/Program/GetList", { PageSize: 1000, TenantId: TenantId }, function (data) {
            if (data.total > 0) {
                $.each(data.rows, function () {
                    htmlStr += ' <option value="' + $(this)[0].Id + '">' + $(this)[0].ProgramName + '</option>';
                })
            }
            $("#" + programContorlId).html(htmlStr);
        }, "json")
    }
}

//秒数显示
function SecondsFormater(val, rowdata, index) {
    return GetSecondsName(val);
}
//比例显示
function P_Formater(val, rowdata, index) {
    return ' <div class="Progress"> <div style="width: ' + (val * 100) + '%; background-color: green;"><span>' + Math.round(val * 100) + '%</span> </div>';
}
//审核状态显示
function FormatterAuditStatus(val, rowdata, index) {
    if (val == "0") {
        return '<label class="labelstatus red">未审核</label>';
    }
    else if (val == "1") {
        return '<label class="labelstatus green">已审核通过</label>';
    }
    else {
        return '<label class="labelstatus gray">审核不通过</label>';
    }
}

//扩展easyui表单的验证
$.extend($.fn.validatebox.defaults.rules, {
    //验证汉字
    CHS: {
        validator: function (value) {
            return /^[\u0391-\uFFE5]+$/.test(value);
        },
        message: '只能输入汉字'
    },
    //移动手机号码验证
    mobile: {//value值为文本框中的值
        validator: function (value) {
            var reg = /^1[3|4|5|8|9]\d{9}$/;
            return reg.test(value);
        },
        message: '输入手机号码格式不准确.'
    },
    //国内邮编验证
    zipcode: {
        validator: function (value) {
            var reg = /^[1-9]\d{5}$/;
            return reg.test(value);
        },
        message: '邮编必须是非0开始的6位数字.'
    },
    //用户账号验证(只能包括 _ 数字 字母)
    account: {//param的值为[]中值
        validator: function (value, param) {
            if (value.length < param[0] || value.length > param[1]) {
                $.fn.validatebox.defaults.rules.account.message = '用户名长度必须在' + param[0] + '至' + param[1] + '范围';
                return false;
            } else {
                if (!/^[\w]+$/.test(value)) {
                    $.fn.validatebox.defaults.rules.account.message = '用户名只能数字、字母、下划线组成.';
                    return false;
                } else {
                    return true;
                }
            }
        }, message: ''
    }
})

$.fn.datebox.defaults.formatter = function (date) {
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
}

$.fn.datebox.defaults.parser = function (s) {
    var t = Date.parse(s);
    if (!isNaN(t)) {
        return new Date(t);
    } else {
        return new Date();
    }
}