layui.config({
    base: "/js/"
}).use(['form','vue', 'ztree', 'layer', 'jquery', 'table','droptree','HLY','utils'], function () {
    var form = layui.form,
        layer = layui.layer,
        $ = layui.jquery;
    var table = layui.table;
    var HLY = layui.HLY;
    var id = $.getUrlParam("id");      //待分配的id
    layui.droptree("/UserSession/GetOrgs", "#Organizations", "#OrganizationIds");
   
    //主列表加载，可反复调用进行刷新
    var config= {};  //table的参数，如搜索key，点击tree的id
    $.getJSON('/RoleManager/Load',
        config,
        function (data) {
            table.render({
                elem: '#mainList',
                cols: [[
                    { checkbox: true, fixed: true },
                    { field: 'Name', title: '角色名称' },
                    { field: 'Status', templet: '#Status', title: '角色状态' },
                    { fixed: 'right', toolbar: '#userList', title: '用户列表' }
                ]],
                data: data.Result,
                height: 'full-80',
                page: true, //是否显示分页
                limits: [10, 20, 50], //显示
                limit: 10, //每页默认显示的数量
                done: function (res, curr, count) {
                    //如果是异步请求数据方式，res即为你接口返回的信息。
                    //如果是直接赋值的方式，res即为：{data: [], count: 99} data为当前页数据、count为数据总长度

                    $.ajax("/RoleManager/LoadForUser?userId=" + id, {
                        async: false
                        , dataType: 'json'
                        , success: function (json) {
                            if (json.Code == 500) return;
                            var roles = json.Result;
                            //循环所有数据，找出对应关系，设置checkbox选中状态
                            for (var i = 0; i < res.data.length; i++) {
                                for (var j = 0; j < roles.length; j++) {
                                    if (res.data[i].Id != roles[j]) continue;

                                    //这里才是真正的有效勾选
                                    res.data[i]["LAY_CHECKED"] = true;
                                    //找到对应数据改变勾选样式，呈现出选中效果
                                    var index = res.data[i]['LAY_TABLE_INDEX'];
                                    $('.layui-table-fixed-l tr[data-index=' + index + '] input[type="checkbox"]').prop('checked', true);
                                    $('.layui-table-fixed-l tr[data-index=' + index + '] input[type="checkbox"]').next().addClass('layui-form-checked');
                                }

                            }

                            //如果构成全选
                            var checkStatus = table.checkStatus('mainList');
                            if (checkStatus.isAll) {
                                $('.layui-table-header th[data-field="0"] input[type="checkbox"]').prop('checked', true);
                                $('.layui-table-header th[data-field="0"] input[type="checkbox"]').next().addClass('layui-form-checked');
                            }
                        }
                    });
                }
            })
        })

    //分配及取消分配
    table.on('checkbox(list)', function (obj) {
        console.log(obj.checked); //当前是否选中状态
        console.log(obj.data); //选中行的相关数据
        console.log(obj.type); //如果触发的是全选，则为：all，如果触发的是单选，则为：one

        var url = "/AccessObjs/Assign";
        if (!obj.checked) {
            url = "/AccessObjs/UnAssign";
        }
        $.post(url, { type: "UserRole", firstId: id, secIds: [obj.data.Id] }
                       , function (data) {
                           layer.msg(data.Message);
                       }
                      , "json");
    });
    //监听页面主按钮操作 end
})