String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}
String.prototype.ltrim = function () {
    return this.replace(/(^\s*)/g, "");
}
String.prototype.rtrim = function () {
    return this.replace(/(\s*$)/g, "");
}

Date.prototype.Format = function (fmt) { //author: meizz
    var o = {
        "M+": this.getMonth() + 1, //月份
        "d+": this.getDate(), //日
        "h+": this.getHours(), //小时
        "m+": this.getMinutes(), //分
        "s+": this.getSeconds(), //秒
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度
        "S": this.getMilliseconds() //毫秒
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
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
function ShowDateTimeFormat(val, rowdata, index) {
    return new Date(val).Format("yyyy-MM-dd hh:mm:ss");
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

/*Jquery.DataTables汉化*/
(function () {
    //    var oLanguage = {
    //        "oAria": {
    //            "sSortAscending": ": 升序排列",
    //            "sSortDescending": ": 降序排列"
    //        },
    //        "oPaginate": {
    //            "sFirst": "首页",
    //            "sLast": "末页",
    //            "sNext": "下页",
    //            "sPrevious": "上页"
    //        },
    //        "sEmptyTable": "没有相关记录",
    //        "sInfo": "第 _START_ 到 _END_ 条记录，共 _TOTAL_ 条",
    //        "sInfoEmpty": "第 0 到 0 条记录，共 0 条",
    //        "sInfoFiltered": "(从 _MAX_ 条记录中检索)",
    //        "sInfoPostFix": "",
    //        "sDecimal": "",
    //        "sThousands": ",",
    //        "sLengthMenu": "每页显示条数: _MENU_",
    //        "sLoadingRecords": "正在载入...",
    //        "sProcessing": "正在载入...",
    //        "sSearch": "搜索:",
    //        "sSearchPlaceholder": "",
    //        "sUrl": "",
    //        "sZeroRecords": "没有相关记录"
    //    }
    $.fn.dataTable.defaults.Language = chineseDataTable;
    //    //$.extend($.fn.dataTable.defaults.oLanguage,oLanguage)
})();
var chineseDataTable = {
    "sProcessing": "处理中...",
    "sLengthMenu": "显示_MENU_项结果",
    "sZeroRecords": "没有匹配结果",
    "sInfo": "显示第_START_至_END_项结果，共_TOTAL_项",
    "sInfoEmpty": "显示第0至0项结果，共0项",
    "sInfoFiltered": "(由_MAX_项结果过滤)",
    "sInfoPostFix": "",
    "sSearch": "搜索:",
    "sUrl": "",
    "sEmptyTable": "表中数据为空",
    "sLoadingRecords": "载入中...",
    "sInfoThousands": ",",
    "oPaginate": {
        "sFirst": "首页",
        "sPrevious": "上页",
        "sNext": "下页",
        "sLast": "末页"
    },
    "oAria": {
        "sSortAscending": ":以升序排列此列",
        "sSortDescending": ":以降序排列此列"
    }
};