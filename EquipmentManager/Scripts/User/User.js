 
//保存
function SaveEntity() {
    $('#fmDetail').form('submit', {
        url: "/User/Save",
        onSubmit: function (param) {                //提交时触发
            var flag = $(this).form('validate');    //是否通过验证
            return flag;
        },
        success: function (res) {
            res = JSON.parse(res);
            if (res.Status) {
                $.messager.show({
                    title: '操作提示',
                    msg: '<img src="/Content/images/jingbaihui/ok.png" />' + '保存成功!',
                    showType: 'show'
                });
                $('#DivAdd').dialog('close');         //关闭弹出框
                $('#fmDetail').form('clear');              //清除表单数据。
                $('#btnSearch').click(); //重新加载数据
            }
            else {
                $.messager.show({
                    title: '保存失败!',
                    msg: '<div class="messager-icon messager-error"></div>' + res.Msg,
                    showType: 'fade'
                });
                return false;
            }
        }
    })
}
//删除
function Delete() {
    $.messager.confirm("操作提示", "您确定删除这条数据吗？", function (data) {
        if (data) {
            var row = $('#grid').datagrid('getSelected'); //获取选中行
            if (!row) {                                       //没有选中行
                $.messager.alert('提示', '请选择一行进行操作!', 'info');
                return false;
            }

            $.ajax({
                url: "/User/Delete",
                type: "POST",
                data: { "Id": row["Id"] },
                success: function (res) {
                    if (res.Status) {
                        $.messager.show({
                            title: '操作提示',
                            msg: '删除成功!',
                            timeout: 2000,
                            showType: 'show'
                        });
                        $('#btnSearch').click();
                    }
                    else {
                        $.messager.show({
                            title: '删除失败！',
                            msg: '<div class="messager-icon messager-error"></div>' + res.Msg,
                            showType: 'fade'
                        });
                        return false;
                    }
                },
                error: function (error) {
                    alert(JSON.stringify(error));
                }
            })
        }
    });
}
//显示添加对话框
function ShowAddDialog() {
    $('#fmDetail').form('load', {
        Id: '',
    });
    $('#DivAdd').dialog({
        title: '增加',
        iconCls: 'icon-add'
    }).dialog('open');
}
//显示编辑态或查看态对话框
function ShowEditOrViewDialog(view) {
    var row = $('#grid').datagrid('getSelected'); //获取选中行
    if (!row) {                                       //没有选中行
        $.messager.alert('提示', '请选择一行进行操作!', 'info');
        return false;
    }
    //取值赋值
    $.ajax({
        url: "/User/Get",
        type: "POST",
        dataType: "json",
        data: { "Id": row["Id"] },
        success: function (res) {
            $('#fmDetail').form('load', {
            	Id:res.Id,//主键
		TenantId:res.TenantId,//租户Id
		Name:res.Name,//用户名
		PassWord:res.PassWord,//密码
		RoleId:res.RoleId,//角色Id
		Email:res.Email,//邮箱
		Phone:res.Phone,//电话
		CreateBy:res.CreateBy,//创建人
		CreateTime:res.CreateTime,//创建时间
		ModifyBy:res.ModifyBy,//修改人
		ModifyTime:res.ModifyTime,//修改时间
		 
            });

            var title = "编辑";
            var iconinfo = "icon-edit";
            if (view != undefined) {
                $("#DivAddToolBar").hide();
                title = "查看信息";
                iconinfo = "icon-search";
            }
            else {
                $("#DivAddToolBar").show();
                title = "修改信息";
                iconinfo = "icon-edit";
            }
            $('#DivAdd').dialog({ iconCls: iconinfo, title: title }).dialog('open');
        },
        error: function (error) {
            alert(JSON.stringify(error));
        }
    });
}
//初始化表格
function InitGrid(queryData) {
    $("#grid").datagrid("uncheckAll").datagrid({
        url: '/User/GetList',
        title: '用户信息',
        iconCls: 'icon-view',
        // height: 650,
        //width: function () { return document.body.clientWidth * 0.5; },
        nowrap: true,
        autoRowHeight: true,
        striped: true,
        collapsible: true,
        pagination: true,
        pageSize: 15,
        pageList: [10, 15, 20],
        rownumbers: true,
        singleSelect: true,
        sortOrder: 'asc',
        //remoteSort: false,
        idField: 'Id',
        queryParams: queryData,  //异步查询的参数
        onLoadSuccess: function (data) {
            if (data.total == 0) {
                //添加一个新数据行，第一列的值为你需要的提示信息，然后将其他列合并到第一列来，注意修改colspan参数为你columns配置的总列数
                //$(this).datagrid('appendRow', { Skt_Factory_Name: '<div style="text-align:center;color:red">没有查询到相关数据！</div>' }).datagrid('mergeCells', { index: 0, field: 'Skt_Factory_Name', colspan: 7 })
                //隐藏分页导航条，这个需要熟悉datagrid的html结构，直接用jquery操作DOM对象，easyui datagrid没有提供相关方法隐藏导航条
                $(this).closest('div.datagrid-wrap').find('div.datagrid-pager').hide();

                $("#emptymsgdiv").show();
            }
                //如果通过调用reload方法重新加载数据有数据时显示出分页导航容器
            else {
                $(this).closest('div.datagrid-wrap').find('div.datagrid-pager').show();
                $("#emptymsgdiv").hide();
            }
        },
        onDblClickRow: function (rowIndex, rowData) {
            $('#grid').datagrid('uncheckAll');
            $('#grid').datagrid('checkRow', rowIndex);
            ShowEditOrViewDialog('view');
        },
        onLoadError: function (error) {
            alert(error);
        }
    });
    var p = $('#grid').datagrid('getPager');
    (p).pagination({
        beforePageText: '第',//页数文本框前显示的汉字
        afterPageText: '页    共 {pages} 页',
        displayMsg: '共{total}条数据',
    });
}

$(function () {
    //回车搜索
    jQuery("#fmSearch").bind("keydown", function (e) {
        var key = e.which;
        if (key == 13) {
            e.stopPropagation();
            $('#btnSearch').click();
            return false;
            //stopPropagation(e);
        }
    });

    $('#btnSearch').click(function () {
        //得到用户输入的参数，取值有几种方式：$("#id").combobox('getValue'), $("#id").datebox('getValue'), $("#id").val()
        var queryData = {};
        var fields = $("#fmSearch").serializeArray();
        jQuery.each(fields, function (i, field) {
            queryData[field.name] = field.value;
        });

        InitGrid(queryData);
        return false;
    });

    $('#btnSearch').click();
})