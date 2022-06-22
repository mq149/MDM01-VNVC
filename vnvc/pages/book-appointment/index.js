$(document).ready(function () {
  var x = document.getElementById("selection_city");
  const city_api = "https://localhost:5001/city";
  $.ajax({
    url: city_api,
    method: "get",
    success: function (response) {
      console.log(response);
      cityListApi(response);
    },
    error: function (errors) {
      console.log(errors);
    },
  });
  function cityListApi(cities) {
    cities.forEach((city, index) => {
      var option = document.createElement("option");
      option.value = city;
      option.text = city;

      /*if (city == "HCM") {
        option.selected = "selected";
      }*/
      x.append(option);
    });
  }
});

$(document).ready(function () {
  let dictrictList = function () {
    const dictrict_api =
      "https://localhost:5001/dictrict/" + $("#selection_city").val();
    console.log("hello", dictrict_api);
    var x = document.getElementById("selection_dictrict");
    x.length = 0;
    var option0 = document.createElement("option");
    option0.value = "None";
    option0.selected = "selected";
    x.append(option0);
    $.ajax({
      url: dictrict_api,
      method: "get",
      success: function dictrictListApi(response) {
        console.log(response);
        response.forEach((dictrict, index) => {
          var option = document.createElement("option");
          option.value = dictrict;
          option.text = dictrict;

          /*if (index == 0) {
            option.selected = "selected";
          }*/
          x.append(option);
        });
      },
      error: function (errors) {
        console.log(errors);
      },
    });
  };

  $("#selection_city").change(dictrictList);
});

$(document).ready(function () {
  let wardList = function () {
    const ward_api =
      "https://localhost:5001/ward/" + $("#selection_dictrict").val();
    console.log("hello", ward_api);
    var x = document.getElementById("selection_ward");
    x.length = 0;
    var option0 = document.createElement("option");
    option0.value = "None";
    option0.selected = "selected";
    x.append(option0);
    $.ajax({
      url: ward_api,
      method: "get",
      success: function wardListApi(response) {
        console.log(response);
        response.forEach((dictrict, index) => {
          var option = document.createElement("option");
          option.value = dictrict;
          option.text = dictrict;

          /*if (index == 0) {
            option.selected = "selected";
          }*/
          x.append(option);
        });
      },
      error: function (errors) {
        console.log(errors);
      },
    });
  };
  $("#selection_dictrict").change(wardList);
  $("#selection_city").change(wardList);
});

$(document).ready(function () {
  $("label.el-radio-button").change(function () {
    if (
      $("input.el-radio-button__orig-radio:checked").val() == "0" ||
      $("input.el-radio-button__orig-radio:checked").val() == "1"
    ) {
      $("input.el-radio-button__orig-radio.is-checked").prop("checked", false);
      $("input.el-radio-button__orig-radio.is-checked").removeClass(
        "is-checked"
      );
      $("input.el-radio-button__orig-radio:checked").addClass("is-checked");
      console.log(
        $("input.el-radio-button__orig-radio.is-checked:checked").val()
      );
    }
  });
});

function Check() {
  var s = "";
  var arr = document.getElementsByName("checkVaccine");

  for (var i = 0; i < arr.length; i++) {
    if (arr[i].checked) s = s + arr[i].value + ",";
  }

  document.getElementById("txtSelectedID").value = s;
}

$(document).ready(function () {
  $("input.check_type").click(function () {
    if (
      $("input.check_type:checked").val() == "package" ||
      $("input.check_type:checked").val() == "retail"
    ) {
      $("input.check_type.is-checked").prop("checked", false);
      $("input.check_type.is-checked").removeClass("is-checked");
      $("input.check_type:checked").addClass("is-checked");
      $("div.vaccines").empty();
      console.log($("input.check_type.is-checked:checked").val());
      if ($("input.check_type.is-checked:checked").val() == "package") {
        let rowHtml = `
              <spand>***Chọn gói vaccine***</spand>
              <br>
              <select class="selction" id="selection_vaccine_type" type="text" name="vaccine_type">
              <option value="None" selected="selected"></option>
            </select>
          `;
        $("div.vaccines").append(rowHtml);
        const danhmuc_api = "https://localhost:5001/VaccinePackage/danhmuc";
        $.ajax({
          url: danhmuc_api,
          method: "get",
          success: function (response) {
            console.log(response);
            response.forEach((danhmuc, index) => {
              var option = document.createElement("option");
              option.value = danhmuc;
              option.text = danhmuc;

              document.getElementById("selection_vaccine_type").append(option);
            });
          },
          error: function (errors) {
            console.log(errors);
          },
        });
      }

      if ($("input.check_type.is-checked:checked").val() == "retail") {
        let rowHtml = `
              <spand>***Chọn vaccine***</spand>
              <br>
              <table id="vaccine-price-list" class="">
              <thead>
                  <tr>
                      <th scope="col"></th>
                      <th scope="col">Phòng bệnh</th>
                      <th class="light-blue" scope="col">Tên vắc xin</th>
                      <th class="light-blue" scope="col">
                          Nước sản xuất
                      </th>
                      <th class="blue" scope="col">Giá bán lẻ (vnđ)</th>
                      <th class="blue" scope="col">
                          Giá đặt giữ theo yêu cầu (vnđ)
                      </th>
                      <th class="blue" scope="col">Tình trạng</th>
                  </tr>
              </thead>
              <tbody></tbody>
              <input id="txtSelectedID" type="hidden" name="vaccineid" value=""/>
          </table>
          `;
        $("div.vaccines").append(rowHtml);
        const vaccines_api = "https://localhost:5001/VaccinePriceList";
        $.ajax({
          url: vaccines_api,
          method: "get",
          success: function (response) {
            console.log(response);
            response.forEach((vaccine, index) => {
              let rowHtml = `
          <tr>
            <th scope="row"><input style='width:80px' name='checkVaccine' type='checkbox' value='${
              vaccine["Id"]
            }' onclick='Check();'/></th>
            <td>${vaccine["ProtectAgainst"]}</td>
            <td class="light-blue">${vaccine["Name"]}</td>
            <td class="light-blue">${vaccine["CountryOfOrigin"]}</td>
            <td class="blue">${vaccine["RetailPrice"].toLocaleString(
              "de-DE"
            )}</td>
            <td class="blue">${vaccine["RetailPrice"].toLocaleString(
              "de-DE"
            )}</td>
            <td class="blue">${vaccine["Status"]}</td>
          </tr>
          `;
              $("table#vaccine-price-list tbody").append(rowHtml);
            });
          },
          error: function (errors) {
            console.log(errors);
          },
        });
      }
    }
  });
});

$(document).ready(function () {
  let a = function () {
    const ward_api = "https://localhost:5001/ward/";
    console.log("hello", ward_api);
    var x = document.getElementById("selection_ward");
    x.length = 0;
    var option0 = document.createElement("option");
    option0.value = "None";
    option0.selected = "selected";
    x.append(option0);
    $.ajax({
      url: ward_api,
      method: "get",
      success: function wardListApi(response) {
        console.log(response);
        response.forEach((dictrict, index) => {
          var option = document.createElement("option");
          option.value = dictrict;
          option.text = dictrict;

          /*if (index == 0) {
            option.selected = "selected";
          }*/
          x.append(option);
        });
      },
      error: function (errors) {
        console.log(errors);
      },
    });
  };
  $("#selection_vaccine_type").change(a);
});
