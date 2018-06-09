let container = document.getElementById('customersTable');
let dataTable = new Handsontable(container, {
    data: [],
    colHeaders: ['Name', 'Email', 'Updated'],
    columns: [
        {
            data: 'Name',
            editor: false
        },
        {
            data: 'Email',
            editor: false
        },
        {
            data: 'DateUpdated',
            editor: false
        }
    ],
    columnSorting: true,
    sortIndicator: true,
    rowHeaders: true,
    filters: true,
    dropdownMenu: true
});

var CustomerHandler = {
    baseUri: 'http://localhost:50076/delltestapi/customer',
    UpdateData: function () {
        $.ajax({
            type: "GET",
            url: this.baseUri + '/getall',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, status) {
                console.log(data);
                console.log(status);
                dataTable.updateSettings({ data: data });
                dataTable.render();
            },
            error: function (xhr) {
                console.log(xhr.responseText);
            }
        });
    },
    Post: function (method) {
        let customerDetails = {
            name: $('#custName').val(),
            email: $('#custEmail').val()
        };

        if (customerDetails.name === '' || customerDetails.email === '') {
            alert('Write something!');
            return false;
        }

        $.ajax({
            type: "POST",
            url: this.baseUri + '/' + method,
            data: JSON.stringify(customerDetails),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            processData: true,
            success: function (data, status) {
                console.log(data);
                console.log(status);

                $('h4').css('color', 'green');
                $('#responseTxt').text(JSON.stringify(data));

                CustomerHandler.UpdateData();
            },
            error: function (xhr) {
                console.log(xhr.responseText);

                $('h4').css('color', 'red');
                $('#responseTxt').text(JSON.stringify(xhr.responseText === '' ? xhr.statusText : xhr.responseText));
            }
        });
    }
};

$(document).ready(function () {
    CustomerHandler.UpdateData();
});

$('#addCustomerBtn').click(function () {
    CustomerHandler.Post('add');
});

$('#updateCustomerBtn').click(function () {
    CustomerHandler.Post('update');
});