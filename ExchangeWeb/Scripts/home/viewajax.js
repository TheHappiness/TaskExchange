/* viewajax.js */

$(document).ready(function () {
    var parsedData = null;
    var maxPrice = null;
    $.ajax({
        url: '/ajaxtrades',
        type: 'post',
        success: function (data) {
            try {
                if (!data.error) {
                    maxPrice = data.maxPrice;
                    parsedData = JSON.parse(data.trades);
                    // create table data
                    parsedData.forEach(function (item, i, arr) {
                        // set price
                        var tr = document.createElement('tr');
                        var pricetd = document.createElement("td");
                        pricetd.innerText = item.price + ' BYN';
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
                        // load chart
                        google.charts.load('current', { 'packages': ['corechart'] });
                        google.charts.setOnLoadCallback(drawChart);                      
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

    function drawChart() {
        try {
            var max = maxPrice;
            var dataForChart = new google.visualization.DataTable();
            dataForChart.addColumn('date', 'Дата');
            dataForChart.addColumn('number', 'Цена');
            var convertedTradesData = parsedData.map(function (name) {
                return [new Date(name.transactionTime), name.price];
            });

            dataForChart.addRows(convertedTradesData);

            var options = {
                title: 'Зависимость цены от времени',
                hAxis: {
                    format: 'dd/MM/yyyy',
                    gridlines: { count: max }
                },
                vAxis: {
                    gridlines: { color: 'none' },
                    minValue: 0
                }
            };

            var chart = new google.visualization.LineChart(document.getElementById('chart_div'));
            chart.draw(dataForChart, options);
        } catch (error) {
            console.error(error.message);
        }
    }
});