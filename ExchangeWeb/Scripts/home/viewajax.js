/* viewajax.js */

$(document).ready(function () {
    var parsedData = null;
    $.ajax({
        url: '/ajaxtrades',
        type: 'post',
        success: function (data) {
            try {
                if (!data.error) {
                    parsedData = JSON.parse(data.trades);
                    // create table data
                    parsedData.forEach(function (item, i, arr) {
                        // set price
                        var tr = document.createElement('tr');
                        var pricetd = document.createElement("td");
                        pricetd.innerText = item.price + 'BYN';
                        tr.appendChild(pricetd);
                        // set volume
                        var volumetd = document.createElement("td");
                        volumetd.innerText = item.volume;
                        tr.appendChild(volumetd);
                        // set date
                        var transactionTimetd = document.createElement("td");
                        transactionTimetd.innerText = item.transactionTime;
                        tr.appendChild(transactionTimetd);
                        // set sellerName
                        var sellerNameetd = document.createElement("td");
                        sellerNameetd.innerText = item.sellerName;
                        tr.appendChild(sellerNameetd);
                        // customerName
                        var customerNametd = document.createElement("td");
                        customerNametd.innerText = item.customerName;
                        tr.appendChild(customerNametd);
                        document.getElementsByTagName('tbody')[0].appendChild(tr);
                    }); 
                }
                else {
                    $('tbody').append('<p>нет данных для показа</p>');
                }
            } catch (error) {
                $('tbody').append('<p>Ошибка данных</p>');
                console.error(error.message);
            }        
        },
        error: function (error) {
            $('tbody').append('<p>Ошибка данных</p>');
            console.error(error.message);
        }
    });

    function drawChart(parsedData) {
        try {
            var maxPrice = parsedData.maxPrice;
            var data = new google.visualization.DataTable();
            data.addColumn('date', 'Дата');
            data.addColumn('number', 'Цена');
            var convertedTradesData = parsedData.trades.map(function (name) {
                return [new Date(name.transactionTime), name.price];
            });

            data.addRows(convertedTradesData);

            var options = {
                title: 'Зависимость цены от времени',
                //width: 900,
                //height: 500,
                hAxis: {
                    format: 'dd/MM/yyyy',
                    gridlines: { count: maxPrice }
                },
                vAxis: {
                    gridlines: { color: 'none' },
                    minValue: 0
                }
            };

            var chart = new google.visualization.LineChart(document.getElementById('chart_div'));
            chart.draw(data, options);
        } catch (error) {
            console.error(error.message);
        }
    }

    setTimeout(function () {
        // load chart
        if (parsedData != null) {
            google.charts.load('current', { 'packages': ['corechart'] });
            google.charts.setOnLoadCallback(drawChart(parsedData));
        }
    }, 1000);
});