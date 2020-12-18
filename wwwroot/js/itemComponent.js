"use strict";

$(() => {
    if ($('#fingers10').length !== 0) {

        var table = $('#fingers10').DataTable({
            "scrollY": "" + (window.outerHeight - 375) + "px",
            "scrollCollapse": true,
            language: {
                processing: "Loading Data...",
                zeroRecords: "No matching records found"
            },
            processing: true,
            serverSide: true,
            orderCellsTop: true,
            searching: true,
            stateSave: true,
            autoWidth: true,
            deferRender: true,
            "pageLength": 10,
            "lengthChange": false,
            dom: '<"row"<"col-sm-12 col-md-6"B><"col-sm-12 col-md-6 text-right"l>><"row"<"col-sm-12"tr>><"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
            buttons: [

            ],
            ajax: {
                type: "POST",
                url: '/Inventory/LoadTable/',
                contentType: "application/json; charset=utf-8",
                async: true,
                data: function (data) {
                    let additionalValues = [];
                    additionalValues[0] = $("#searchd").val();
                    data.AdditionalValues = additionalValues;
                    return JSON.stringify(data);
                },
                beforeSend: function () {
                    // Here, manually add the loading message.
                    $('#fingers10 > tbody').html(
                        '<tr class="odd">' +
                        '<td valign="top" colspan="10" class="dataTables_empty">Loading&hellip;</td>' +
                        '</tr>'
                    );
                },
            },
            columns: [
                {
                    data: "indSell_ItemMaster",
                    name: "co",
                    render: function (data, type, row) {
                        return row.indSell_ItemMaster + '(' + row.Item_master +')';

                    } 
                },
                {
                    data: "indSell_ItemComponent",
                    name: "co",
                    render: function (data, type, row) {
                        return row.indSell_ItemComponent + '(' + row.Item_component + ')';

                    } 
                },
                {
                    data: "Allowed",
                    searchable: false,
                    name: "co",
                    render: function (data, type, row) {
                        if (row.indSell_Allowed) {
                            return `<span class="badge badge-success">Yes</span>`;
                        }
                        else {
                            return `<span class="badge badge-danger">No</span>`;
                        }

                    }
                },
                {
                    orderable: false,

                    data: "Action",
                    render: function (data, type, row) {

                        return `<div>
                                    <a   href="#" class="btnEdit" data-key="${row.indSell_ItemMaster}">Edit</button>
                                 </div>`;
                    }
                },
                {
                    data: "Item_master",
                    visible: false
                }
                ,
                {
                    data: "Item_component",
                    visible: false
                }

            ],
            rowsGroup: [0],
            "drawCallback": function (settings) {
               
                //try {
                //    if (table) {
                    
                //        table.rowGroup().enable().draw();
                //    }
                //} catch (e) {

                //}
                
               
            }
        });



        table.columns().every(function (index) {
            $('#fingers10 thead tr:last th:eq(' + index + ') input')
                .on('keyup',
                    function (e) {
                        if (e.keyCode === 13) {
                            table.column($(this).parent().index() + ':visible').search(this.value).draw();
                        }
                    });
        });

        $("input").on("change", function (e) {
            table.draw();
        });

        $(document)
            .off('click', '.btnEdit')
            .on('click', '.btnEdit', function () {
                const id = $(this).attr('data-key');
                window.location.href = window.baseUrl + "/Inventory/EditItemComponent/" + id;

            });


    }
});
