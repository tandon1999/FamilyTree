function LoadDatePicker(id, type) {
    if (id == undefined) {
        if (type == "NP") {
            var mainInput = document.getElementsByClassName("np-datepicker");
            mainInput.nepaliDatePicker({
                ndpYear: true,
                ndpMonth: true,
                dateFormat: "YYYY-MM-DD",
                onChange: function () {
                    var event = new Event("change");
                    mainInput.dispatchEvent(event);
                }
            });
        }
        else if (type == "EN") {
            $("np-datepicker").datepicker({
                beforeShow: function (input, inst) {
                    $('#ui-datepicker-div').attr("inputId", this.id);
                }
            });
        }

    }
    else {
        if (type == "NP") {
            var mainInput = document.getElementById(id);
            mainInput.nepaliDatePicker({
                ndpYear: true,
                ndpMonth: true,
                dateFormat: "YYYY/MM/DD",
                onChange: function () {
                    var event = new Event("change");
                    mainInput.dispatchEvent(event);
                }
            });
        }
        else if (type == "EN") {
            $("#" + id).datepicker('destroy');
            $("#" + id).datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'yy-mm-dd',
                onSelect: function (date) {
                    var myElement = $(this)[0];
                    var event = new Event('change');
                    myElement.dispatchEvent(event);
                }
            });
        }
    }
};

function CheckDate(input) {
    var len = input.length;
    var isFound = input.indexOf("-") != -1 ? true : false;
    debugger;
    switch (len) {
        case 4: {
            if (!isFound) {
                date = input + ("-");
                return date;
            }
        }
        case 7: {
            date = input + ("-");
            return date;
        }

        default: {
            date = input
            return date;
        }

    }
};