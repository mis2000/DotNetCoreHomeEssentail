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
                    return JSON.stringify(data);
                }
            },
            columns: [
                {
                    data: "indSell_ItemMaster",
                    name: "co"
                },
                {
                    data: "indSell_ItemComponent",
                    name: "co"
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
                }

            ]
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



        $(document)
            .off('click', '.btnEdit')
            .on('click', '.btnEdit', function () {
                const id = $(this).attr('data-key');
                debugger;
                window.location.href = window.baseUrl + "/Inventory/EditItemComponent/" + id;

            });


    }
});
