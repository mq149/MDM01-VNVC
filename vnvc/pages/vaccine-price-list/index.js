$(function () {
    console.log("hello world");

    const vaccine_price_list_api = "https://localhost:5001/vaccinepricelist";

    $.ajax({
        url: vaccine_price_list_api,
        method: "get",
        success: function (response) {
            console.log(response);
            populateVaccinePriceTable(response);
        },
        error: function (errors) {
            console.log(errors);
        },
    });

    function populateVaccinePriceTable(vaccines) {
        vaccines.forEach((vaccine, index) => {
            let rowHtml = `
          <tr>
            <th scope="row">${index + 1}</th>
            <td>${vaccine["protectAgainst"]}</td>
            <td class="light-blue">${vaccine["name"]}</td>
            <td class="light-blue">${vaccine["countryOfOrigin"]}</td>
            <td class="blue">${vaccine["retailPrice"].toLocaleString(
                "de-DE"
            )}</td>
            <td class="blue">${vaccine["preOrderPrice"].toLocaleString(
                "de-DE"
            )}</td>
            <td class="blue">${vaccine["status"]}</td>
          </tr>
          `;
            $("table#vaccine-price-list tbody").append(rowHtml);
        });
    }
});
