"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var $ = require("jquery");
//globalne rowId zeby moc dodawac nowe itemy do faktury
//teraz nie dziala....
function DeleteRow(id) {
    $("#" + id).hide();
}
function EnableSelect2() {
    $('.product').select2({
        theme: "bootstrap-5",
        width: 'style',
        tags: true
    });
}
function AddItem() {
    if (window.window._rowId === undefined)
        window.window._rowId = 0;
    var productList = $("#viewBagProductList").val();
    var taxesList = $("#viewBagTaxTypes").val();
    var quantityUnits = $("#viewBagQuantityUnits").val();
    var productId = "product_" + window._rowId;
    var html = '<div class="row m-0 p-0 mt-3" id="' + window._rowId + '"><div class="col-sm-5"><select onchange="GetProductDetail(' + window._rowId + ')" id="' + productId + '" class="product w-100" name="state"><option selected="selected"></option></select></div><div class="col-sm-1"><input id="quantity_' + window._rowId + '" onchange="UpdateTotalPrice(' + window._rowId + ')" autocomplete="off" class="form-control" /></div><div class="col-sm-1 "><select id="quantityUnit_' + window._rowId + '" class="form-select"></select></div><div class="col-sm-1"><input id="netPrice_' + window._rowId + '" onchange="UpdateTotalPrice(' + window._rowId + ')" autocomplete="off" class="form-control" /></div><div class="col-sm-1"><select id="tax_' + window._rowId + '" onchange="UpdateTotalPrice(' + window._rowId + ')" class="form-select"></select></div><div class="col-sm-1"><input id="totalNet_' + window._rowId + '" autocomplete="off" class="form-control" /></div><div class="col-sm-1"><input id="totalGross_' + window._rowId + '" autocomplete="off" class="form-control" /></div><div class="col-sm-1" ><a onclick="DeleteRow(' + window._rowId + ')" style="font-size: 2rem; color: red;vertical-align: top; cursor: pointer" title="Delete entry"><i class="bi bi-x-square-fill"></i></a></div></div>';
    var productContainer = document.getElementById("productContainer");
    $('#productContainer').append(html);
    EnableSelect2();
    $.each(JSON.parse(productList), function (i, item) {
        var newOption = new Option(item.Name, item.Id, false, false);
        $('#' + productId).append(newOption).trigger('change');
    });
    $.each(JSON.parse(taxesList), function (i, item) {
        $('#tax_' + window._rowId).append($('<option>', {
            value: item.Tax,
            text: item.Tax
        }));
    });
    $.each(JSON.parse(quantityUnits), function (i, item) {
        $('#quantityUnit_' + window._rowId).append($('<option>', {
            value: item,
            text: item
        }));
    });
    window._rowId += 1;
}
function GetProductDetail(id) {
    var productList = $("#viewBagProductList").val();
    $("#product_" + id).on("change", function () {
        var productId = $("#product_" + id).find(":selected").val();
        $.each(JSON.parse(productList), function (i, item) {
            if (productId != "") {
                if (item.Id == productId) {
                    $("#netPrice_" + id).val(item.NetPrice);
                    $("#tax_" + id).val(item.Tax);
                    $("#quantity_" + id).val("1");
                    $("#quantityUnit_" + id).val(item.QuantityUnit);
                    UpdateTotalPrice(id);
                }
            }
            else {
                $("#netPrice_" + id).val("");
                $("#tax_" + id).val("");
            }
        });
    });
}
function UpdateBuyer() {
    var contractorId = $("#contractorId").val();
    if (contractorId != "" && contractorId != null && contractorId != 0) {
        $.ajax({
            type: 'POST',
            cache: false,
            url: '/Contractor/Get?contractorId=' + contractorId,
            dataType: 'json',
            contentType: 'application/json',
            success: function (contractor) {
                $.each(contractor, function (k, v) {
                    if (k != null && k != "") {
                        if (v != null && v != "") {
                            $("#" + k).val(v);
                        }
                    }
                });
            },
            error: function () {
            }
        });
    }
}
function UpdateTotalPrice(id) {
    var test = $("#quantity_" + id).val();
    var quantity = parseInt($("#quantity_" + id).val().toString());
    var netPrice = parseFloat($("#netPrice_" + id).val().toString());
    var tax = parseInt($("#tax_" + id).val().toString());
    var totalNet = quantity * netPrice;
    var totalGross = totalNet + (totalNet * (tax / 100));
    $("#totalNet_" + id).val(totalNet);
    $("#totalGross_" + id).val(totalGross);
}
function CreateInvoice() {
    var listOfProducts = [];
    $('#productContainer').children().each(function () {
        var id = $(this).attr('id');
        var display = document.getElementById(id).style.display;
        if (display != "none") {
            var product = {
                id: $("#product_" + id).val(),
                netPrice: $("#netPrice_" + id).val(),
                totalNet: $("#totalNet_" + id).val(),
                totalGross: $("#totalGross_" + id).val(),
                quantity: $("#quantity_" + id).val(),
                quantityUnit: $("#quantityUnit_" + id).val(),
                tax: $("#tax_" + id).val(),
            };
            listOfProducts.push(product);
        }
    });
    var invoice = {
        invoiceTypeId: $("#invoiceTypeId").val(),
        number: $("#invoiceNumber").val(),
        saleDate: $("#saleDate").val(),
        creationDate: $("#creationDate").val(),
        contractorId: $("#contractorId").val(),
        products: listOfProducts
    };
    if (listOfProducts.length != 0) {
        $.ajax({
            type: 'POST',
            dataType: 'JSON',
            url: '/Invoice/Create',
            data: { invoice: JSON.stringify(invoice) },
            success: function (response) {
                //window.location.href = response.redirectToUrl;
            }
        });
    }
}
function Validate() {
    if ($('#productContainer').length == 0) {
        alert("Ad prouct to invoice");
        return false;
    }
    return true;
}
//# sourceMappingURL=invoice.js.map