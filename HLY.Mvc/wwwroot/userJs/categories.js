layui.config({
    base: "/js/"
}).use(['form','vue', 'ztree', 'layer', 'jquery', 'table','droptree','HLY','utils'], function () {
    var form = layui.form,
        layer = layui.layer,
        $ = layui.jquery;
    var table = layui.table;
    var HLY = layui.HLY;

    $("#menus").loadMenus("Category");

    //加载表头
    $.getJSON('/Categories/All',
	    { page: 1, limit: 1 },
	    function(data) {
            var columns = data.columnFields.filter(u => u.IsList ===true).map(function (e) {
                return {
                    field: e.ColumnName,
                    title: e.Comment
                };
            });
		    columns.unshift({
			    type: 'checkbox',
			    fixed: 'left'
		    });
		    table.render({
               elem: '#mainList',
               page: true,
               url: '/Categories/All',
               cols: [columns]
               , response: {
	               statusCode: 200 //规定成功的状态码，默认：0
               } 
		    });
	    });
   
    //主列表加载，可反复调用进行刷新
    var config= {};  //table的参数，如搜索key，点击tree的id
    var mainList = function (options) {
        if (options != undefined) {
            $.extend(config, options);
        }
        table.reload('mainList', {
            url: '/Categories/All',
            where: config
            , response: {
                statusCode: 200 //规定成功的状态码，默认：0
            } 
        });
    }

    //添加（编辑）对话框
    var editDlg = function() {
        var vm;
        var update = false;  //是否为更新
        var show = function (data) {
            var title = update ? "编辑信息" : "添加";
            layer.open({
                title: title,
                area: ["500px", "400px"],
                type: 1,
                content: $('#divEdit'),
                success: function() {
                    if(vm == undefined){
                        vm = new Vue({
                            el: "#formEdit",
                            data(){
                                return {
                                    tmp:data  //使用一个tmp封装一下，后面可以直接用vm.tmp赋值
                                }
                            },
                            watch:{
                                tmp(val){
                                    this.$nextTick(function () {
                                       form.render();  //刷新select等
                                   })
                                }
                            },
                            mounted(){
                                 form.render();
                                var _this = this;
                                layui.droptree("/Categories/AllTypes", "#TypeName", "#TypeId", false,function (ids, names) {
                                    _this.tmp.TypeName = ids;
                                    _this.tmp.TypeId = names;
                                });
                            }
                        });
                       }else{
                        vm.tmp = Object.assign({}, vm.tmp,data)
                       }
                },
                end: mainList
            });
            var url = "/Categories/Add";
            if (update) {
                url = "/Categories/Update"; //暂时和添加一个地址
            }
            //提交数据
            form.on('submit(formSubmit)',
                function(data) {
                    $.post(url,
                        data.field,
                        function(data) {
                            layer.msg(data.Message);
                        },
                        "json");
                    return false;
                });
        }
        return {
            add: function() { //弹出添加
                update = false;
                show({
                    Id: ''
                });
            },
            update: function(data) { //弹出编辑框
                update = true;
                show(data);
            }
        };
    }();
    
    //监听表格内部按钮
    table.on('tool(list)', function (obj) {
        var data = obj.data;
        if (obj.event === 'detail') {      //查看
            layer.msg('ID：' + data.Id + ' 的查看操作');
        } 
    });


    //监听页面主按钮操作
    var active = {
        btnDel: function () {      //批量删除
            var checkStatus = table.checkStatus('mainList')
                , data = checkStatus.data;
            HLY.del("/Categories/Delete",
                data.map(function (e) { return e.Id; }),
                mainList);
        }
        , btnAdd: function () {  //添加
            editDlg.add();
        }
         , btnEdit: function () {  //编辑
             var checkStatus = table.checkStatus('mainList')
               , data = checkStatus.data;
             if (data.length != 1) {
                 layer.msg("请选择编辑的行，且同时只能编辑一行");
                 return;
             }
             editDlg.update(data[0]);
         }

        , search: function () {   //搜索
            mainList({ key: $('#key').val() });
        }
        , btnRefresh: function() {
            mainList();
        }
    };

    $('.toolList .layui-btn').on('click', function () {
        var type = $(this).data('type');
        active[type] ? active[type].call(this) : '';
    });

    //监听页面主按钮操作 end
})