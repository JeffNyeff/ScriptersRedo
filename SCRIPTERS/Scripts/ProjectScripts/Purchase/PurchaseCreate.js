///<reference path="~/Theme/bower_components/jquery/dist/jquery.min.js" />
$(document).ready(function () {
    $("#PurchaseDate").datepicker({
        autoclose: true
    });

    $("#AddButton").click(function () {
        var costPrice = $("#ItemPrice").val();
        var sellingPrice = $("#sellingPrice").val();
        var itemName = $("#ItemName").val();
        var quantity = $("#ItemQuantity").val();

        // alert(costPrice);
        if (itemName == "" || quantity == "" || quantity==0 || costPrice == "" || costPrice >= sellingPrice) {





            //validation to check if we are making profit
            if (costPrice >= sellingPrice) {
                $(".message_box").html(
                    '<span style="color:red;">Cost price is greater than Selling price, we must make a profit. </span>'
                );
                $(".message_box").show();

            }
            //input validation
            if (costPrice == "") {
                $(".message_box").html(
                    '<span style="color:red;">Please provide cost price </span>'
                );
                $(".message_box").show();
                $("#ItemPrice").focus();
            }
            if (quantity == "" || quantity ==0) {
                $(".message_box").html(
                    '<span style="color:red;">Please provide quantity </span>'
                );
                $(".message_box").show();
                $("#ItemQuantity").focus();
            }
            if (itemName === "") {
                $(".message_box").html(
                    '<span style="color:red;">Select Item from DropDown </span>'
                );
                $(".message_box").show();
                $("#ItemName").focus();
            }
        }

        else {
            $(".message_box").hide();
            CreateRowForPurchase();
            TotalAmount();
        }

    });
});

$("#ItemName").on("change",
    function () {

        var id = $("#ItemName").val();
        $.ajax({
            url: "/Purchases/GetItemSalesPrice",
            type: "post",
            data: { id: id },
            success: function (response) {
                $("#sellingPrice").val(response);
            }
        });


    });

function CreateRowForPurchase() {
    var SelectedItem = GetSelectedItem();

    var index = $("#PurchaseDetailsTable").children("tr").length;
    var sl = index;

    var indexCell = "<td style='display:none'> <input type='hidden' id='index" + index + "' name='PurchaseDetail.index' value='" + index + "'/> </td>"
    var serialCell = "<td>" + (++sl) + "</td>";

    var itemNameCell = "<td> <input type='hidden' id='ItemName" + index + "' name='PurchaseDetail[" + index + "].ItemId' value='" + SelectedItem.ItemName + "' />" + SelectedItem.itemText + " </td>";
    var itemQuantityCell = "<td> <input type='hidden' id='ItemQuantity" + index + "' name='PurchaseDetail[" + index + "].Quantity' value='" + SelectedItem.ItemQuantity + "' />" + SelectedItem.ItemQuantity + " </td>";
    var itemPriceCell = "<td> <input type='hidden' id='ItemPrice" + index + "' name='PurchaseDetail[" + index + "].Price' value='" + SelectedItem.ItemPrice + "' />" + SelectedItem.ItemPrice + " </td>";
    var LineTotalCell = "<td class='total'>" + SelectedItem.ItemQuantity * SelectedItem.ItemPrice + " </td>";
    var actionCell = "<td>" + "<input type='button' class='btn btn-danger btn-sm' value='Remove' onclick='GetDeleteId(" + index + ")' id='" + index + "'/>" + "</td>";
    var createNewRow = "<tr id='DelRow_" + index + "'> " + indexCell + serialCell + itemNameCell + itemQuantityCell + itemPriceCell + LineTotalCell + actionCell + " </tr>";

    $("#PurchaseDetailsTable").append(createNewRow);
    $("#ItemName").val("");
    $("#ItemQuantity").val("");
    $("#ItemPrice").val("");

}

var GetDeleteId = function (Id) {
    $("#DelRow_" + Id).remove();
    TotalAmount();

}

function GetSelectedItem() {

    var ItemName = $("#ItemName").val();
    var ItemQuantity = $("#ItemQuantity").val();
    var ItemPrice = $("#ItemPrice").val();
    var itemText = $("#ItemName option:selected").text();
    var Item = {
        "ItemName": ItemName,
        "ItemQuantity": ItemQuantity,
        "ItemPrice": ItemPrice,
        "itemText": itemText
    }
    return Item;
}

function TotalAmount() {
    var sumOfTotal = 0;
    if ($("#PurchaseDetailsTable").children("tr").length == 0) {
        $("#Total").val(0);
    }
    else {
        $("#PurchaseDetailsTable tr ").each(function (index, value) {
            var Total = parseFloat(($(this).find(".total").html()));
            sumOfTotal = sumOfTotal + Total;
            $("#Total").val(sumOfTotal);
            $("#DueAmount").val(sumOfTotal);
        });
    }
}
