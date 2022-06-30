$(document).ready(function () {
  let rowHtml = `
              <spand>***Chọn gói vaccine***</spand>
              <br>
              <select class="selction" id="selection_vaccinePackage" type="text" name="vaccines">
              <option value="None" selected="selected"></option>
            </select>
          `;
  $("div.vaccines").append(rowHtml);
  const danhmuc_api = "https://localhost:5001/VaccinePackage/danhmuc";
  $.ajax({
    url: danhmuc_api,
    method: "get",
    success: function (response) {
      // console.log(response);
      response.forEach((danhmuc, index) => {
        var option = document.createElement("option");
        option.value = danhmuc["Id"];
        option.text = danhmuc["DanhMuc"];
        document.getElementById("selection_vaccinePackage").append(option);
      });
    },
    error: function (errors) {
      console.log(errors);
    },
  });
});

$(document).on("change", "#selection_vaccinePackage", function () {
  const vaccinePackage_api =
    "https://localhost:5001/VaccinePackage/danhmuc/" +
    $("#selection_vaccinePackage").val();
  // console.log("hello", vaccinePackage_api);
  $("div#Package").empty();

  let rowHtml = `
  <div id="Package">

  <br>
  <table id="vaccine-price-list" class="">
  <thead>
      <tr>
         
          <th class="light-blue" scope="col">Tên Gói</th>
          <th class="light-blue" scope="col">Tên Loại Gói </th>
          <th class="" scope="col">Phòng Bệnh</th>
          <th class="blue" scope="col">
              Số Liều Vaccine
          </th>
          <th class="blue" scope="col">Giá Gói (vnđ)</th>
      </tr>
  </thead>
  <tbody></tbody>
  <input id="txtSelectedID" type="hidden" name="txtSelectedID" value=""/>
  <input id="txtSelectedName" type="hidden" name="txtSelectedName" value=""/>
</table>
</div>
`;
  $("div.vaccines").append(rowHtml);
  $.ajax({
    url: vaccinePackage_api,
    method: "get",
    success: function vaccinePackageApi(response) {
      // console.log(response);
      response.forEach((vaccinePackage, index) => {
        let rowHtml = `
        <tr>
          
          <td class="light-blue" ><input type="hidden" name='packageName' id='packageName' value='${
            vaccinePackage["GoiVaccine"]
          }' /> ${vaccinePackage["GoiVaccine"]}</td>
          <td class="light-blue" ><input type="hidden" name='packageTypeName' id='packageTypeName' value='${
            vaccinePackage["LoaiGoi"]
          }'/> ${vaccinePackage["LoaiGoi"]}</td>
          <td class="" style="text-align: left;">${
            vaccinePackage["Describe"]
          }</td>
          <td class="blue">${vaccinePackage["TotalCount"]}</td>
          <td class="blue"><input type="hidden" name='priceVaccine' id='priceVaccine' value='${
            vaccinePackage["TotalPrice"]
          }'/>${vaccinePackage["TotalPrice"].toLocaleString("de-DE")}</td>
        </tr>
        `;
        $("table#vaccine-price-list tbody").append(rowHtml);
      });
    },
    error: function (errors) {
      console.log(errors);
    },
  });
});
