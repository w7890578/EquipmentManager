/**
* 刷新tab
*
*example: {tabTitle:'tabTitle',url:'refreshUrl'}
*如果tabTitle为空，则默认刷新当前选中的tab
*如果url为空，则默认以原来的url进行reload
*/
function refreshTab(cfg) {
    var refresh_tab = cfg.tabTitle ? $('#tb').tabs('getTab', cfg.tabTitle) : $('#tb').tabs('getSelected');
    if (refresh_tab && refresh_tab.find('iframe').length > 0) {
        var _refresh_ifram = refresh_tab.find('iframe')[0];
        var refresh_url = cfg.url ? cfg.url : _refresh_ifram.src;
        //_refresh_ifram.src = refresh_url;
        _refresh_ifram.contentWindow.location.href = refresh_url;
    }
}
function MenuClick(url, title) {
    $("#CenterFrame").attr("src", url);
    //tb
    //var content = '<div style="width:100%;height:100%;"><iframe scrolling="auto"  framespacing=0 marginheight=0 marginwidth=0 frameborder="0" id="ParentFrame"   src="' + url + '" style="width:100%;height:99%;"></iframe></div>';
    //$("#tb").html(content);

    //if ($('#tb').tabs('exists', title)) {
    //    $('#tb').tabs('select', title);
    //    refreshTab({ tabTitle: title, url: url });
    //} else {
    //    //window.screen.availWidth 返回当前屏幕宽度(空白空间)
    //    //window.screen.availHeight 返回当前屏幕高度(空白空间)
    //    var height = window.screen.availHeight - 210;
    //    var width = window.screen.availWidth - 215;
    //    var content = '<div style="width:100%;height:100%;"><iframe scrolling="auto"  framespacing=0 marginheight=0 marginwidth=0 frameborder="0" id="ParentFrame"   src="' + url + '" style="width:100%;height:99%;"></iframe></div>';
    //    //var content = '<iframe scrolling="auto"  framespacing=0 marginheight=0 marginwidth=0 frameborder="0"   src="' + url + '" style="width:'+width+'px;height:'+height+'px;overflow:scroll;"></iframe>';
    //    if (url == '') {
    //        content = "功能开发中......";
    //    }
    //    $('#tb').tabs('add', {
    //        title: title,
    //        content: content,
    //        closable: true,
    //        width: 'auto',
    //        height: 'auto',
    //        padding: '0px'//,
    //        //tools: [{
    //        //    iconCls: 'icon-nav-example'
    //        //    //,
    //        //    //handler:function(){
    //        //    //    alert('refresh');
    //        //    //}
    //        //}]
    //    });

    //$('#tb').tabs('add', {
    //    title: title,
    //    href: url,
    //    closable: true
    //});
    //}
}
$(function () {
    //$('#tb').tabs({
    //    height: $("#tb").parent().height(),
    //    width: "auto"
    //});
    //  <link href="~/Plug/themes/default/easyui.css" rel="stylesheet" id="linkThemes" />
    $("#changethemes").change(function () {
        var themes = $(this).val();
        $("#linkThemes").attr("href", "/Plug/themes/" + $(this).val() + "/easyui.css");

        var allTabs = $('#tb').tabs('tabs');
        $.each(allTabs, function () {
            var tab = this;
            tab.find('iframe').contents().find("#linkThemes").attr("href", "/Plug/themes/" + themes + "/easyui.css");
        });
        $.post("/Home/ChangeThemes", { "themesName": themes }, function (result) {
            if (result != "ok") {
                alert(result);
            }
        });
    });
    //$("#changethemes").change();

    var tabsId = 'tb';//tabs页签Id
    var tab_rightmenuId = 'tab_rightmenu';//tabs右键菜单Id

    //绑定tabs的右键菜单
    $("#" + tabsId).tabs({
        onContextMenu: function (e, title) {//这时去掉 tabsId所在的div的这个属性：class="easyui-tabs"，否则会加载2次
            e.preventDefault();
            $('#' + tab_rightmenuId).menu('show', {
                left: e.pageX,
                top: e.pageY
            }).data("tabTitle", title);
        }
    });

    //实例化menu的onClick事件
    $("#" + tab_rightmenuId).menu({
        onClick: function (item) {
            CloseTab(tabsId, tab_rightmenuId, item.name);
        }
    });
});

/**
    tab关闭事件
    param	tabId		tab组件Id
param	tabMenuId	tab组件右键菜单Id
param	type		tab组件右键菜单div中的name属性值
*/
function CloseTab(tabId, tabMenuId, type) {
    //tab组件对象
    var tabs = $('#' + tabId);
    //tab组件右键菜单对象
    var tab_menu = $('#' + tabMenuId);

    //获取当前tab的标题
    var curTabTitle = tab_menu.data('tabTitle');

    //关闭当前tab
    if (type === 'tab_menu-tabclose') {
        //通过标题关闭tab
        tabs.tabs("close", curTabTitle);
    }

        //关闭全部tab
    else if (type === 'tab_menu-tabcloseall') {
        //获取所有关闭的tab对象
        var closeTabsTitle = getAllTabObj(tabs);
        //循环删除要关闭的tab
        $.each(closeTabsTitle, function () {
            var title = this;
            tabs.tabs('close', title);
        });
    }

        //关闭其他tab
    else if (type === 'tab_menu-tabcloseother') {
        //获取所有关闭的tab对象
        var closeTabsTitle = getAllTabObj(tabs);
        //循环删除要关闭的tab
        $.each(closeTabsTitle, function () {
            var title = this;
            if (title != curTabTitle) {
                tabs.tabs('close', title);
            }
        });
    }

        //关闭当前左侧tab
    else if (type === 'tab_menu-tabcloseleft') {
        //获取所有关闭的tab对象
        var closeTabsTitle = getLeftToCurrTabObj(tabs, curTabTitle);
        //循环删除要关闭的tab
        $.each(closeTabsTitle, function () {
            var title = this;
            tabs.tabs('close', title);
        });
    }

        //关闭当前右侧tab
    else if (type === 'tab_menu-tabcloseright') {
        //获取所有关闭的tab对象
        var closeTabsTitle = getRightToCurrTabObj(tabs, curTabTitle);
        //循环删除要关闭的tab
        $.each(closeTabsTitle, function () {
            var title = this;
            tabs.tabs('close', title);
        });
    }
}

/**
    获取所有关闭的tab对象
 param	tabs	tab组件
*/
function getAllTabObj(tabs) {
    //存放所有tab标题
    var closeTabsTitle = [];
    //所有所有tab对象
    var allTabs = tabs.tabs('tabs');
    $.each(allTabs, function () {
        var tab = this;
        var opt = tab.panel('options');
        //获取标题
        var title = opt.title;
        //是否可关闭 ture:会显示一个关闭按钮，点击该按钮将关闭选项卡
        var closable = opt.closable;
        if (closable) {
            closeTabsTitle.push(title);
        }
    });
    return closeTabsTitle;
}

/**
    获取左侧第一个到当前的tab
param	tabs		tab组件
param	curTabTitle	到当前的tab
*/
function getLeftToCurrTabObj(tabs, curTabTitle) {
    //存放所有tab标题
    var closeTabsTitle = [];
    //所有所有tab对象
    var allTabs = tabs.tabs('tabs');
    for (var i = 0; i < allTabs.length; i++) {
        var tab = allTabs[i];
        var opt = tab.panel('options');
        //获取标题
        var title = opt.title;
        //是否可关闭 ture:会显示一个关闭按钮，点击该按钮将关闭选项卡
        var closable = opt.closable;
        if (closable) {
            //alert('title' + title + '  curTabTitle:' + curTabTitle);
            if (title == curTabTitle) {
                return closeTabsTitle;
            }
            closeTabsTitle.push(title);
        }
    }
    return closeTabsTitle;
}

/**
获取当前到右侧最后一个的tab
param	tabs		tab组件
param	curTabTitle	到当前的tab
*/
function getRightToCurrTabObj(tabs, curTabTitle) {
    //存放所有tab标题
    var closeTabsTitle = [];
    //所有所有tab对象
    var allTabs = tabs.tabs('tabs');
    for (var i = (allTabs.length - 1) ; i >= 0; i--) {
        var tab = allTabs[i];
        var opt = tab.panel('options');
        //获取标题
        var title = opt.title;
        //是否可关闭 ture:会显示一个关闭按钮，点击该按钮将关闭选项卡
        var closable = opt.closable;
        if (closable) {
            //alert('title' + title + '  curTabTitle:' + curTabTitle);
            if (title == curTabTitle) {
                return closeTabsTitle;
            }
            closeTabsTitle.push(title);
        }
    }
    return closeTabsTitle;
}