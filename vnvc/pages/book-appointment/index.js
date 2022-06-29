function Check() {
  if (document.getElementById("total") != null)
    document.getElementById("total").remove();
  var s = "";
  var names = "";
  let totalPrice = 0,
    totalCount = 0;
  var arr = document.getElementsByName("checkVaccine");
  var name01 = document.getElementsByName("package-name");
  var name02 = document.getElementsByName("package-type-name");
  var prices = document.getElementsByName("priceVaccine");

  for (var i = 0; i < arr.length; i++) {
    if (arr[i].checked) {
      names =
        names +
        $("#selection_vaccinePackage option:selected").text() +
        name01[i] +
        name02[i] +
        ",";
      s = s + arr[i].value + ",";
      var p = 1 * prices[i].value;
      totalPrice += p;
    }
  }
  let rowHtml = `
          <tr id='total'>
            <th scope="row" style="text-align: center;">Total Price</th>
            <td></td>
            <td class="light-blue"></td>
            <td class="light-blue"></td>
            <td class="blue"</td>
            <td class="blue">${totalPrice.toLocaleString("de-DE")}</td>
          </tr>
          `;
  $("table#vaccine-price-list tbody").append(rowHtml);
  document.getElementById("txtSelectedID").value = s;
  document.getElementById("txtSelectedName").value = names;
}

$(document).ready(function () {
  var x = document.getElementById("selection_city");
  const city_api = "https://provinces.open-api.vn/api/";
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
      option.value = city.code;
      option.text = city.name;

      /*if (city == "HCM") {
        option.selected = "selected";
      }*/
      x.append(option);
    });
  }
});

$(document).ready(function () {
  let dictrictList = function () {
    var x = document.getElementById("selection_dictrict");
    x.length = 0;
    if ($("#selection_city").val() != "") {
      const dictrict_api =
        "https://provinces.open-api.vn/api/p/" +
        $("#selection_city").val() +
        "/?depth=2";
      console.log("hello", dictrict_api);
      var option0 = document.createElement("option");
      option0.value = "";
      option0.selected = "selected";
      x.append(option0);

      $.ajax({
        url: dictrict_api,
        method: "get",
        success: function dictrictListApi(response) {
          console.log(response);
          response["districts"].forEach((dictrict, index) => {
            var option = document.createElement("option");
            option.value = dictrict.code;
            option.text = dictrict.name;

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
      console.log($("#selection_city option:selected").text());
    }
  };

  $("#selection_city").change(dictrictList);
});

$(document).ready(function () {
  let wardList = function () {
    var x = document.getElementById("selection_ward");
    x.length = 0;
    if ($("#selection_dictrict").val() != "") {
      const ward_api =
        "https://provinces.open-api.vn/api/d/" +
        $("#selection_dictrict").val() +
        "/?depth=2";
      console.log("hello", ward_api);
      var option0 = document.createElement("option");
      option0.value = "";
      option0.selected = "selected";
      x.append(option0);
      $.ajax({
        url: ward_api,
        method: "get",
        success: function wardListApi(response) {
          console.log(response);
          response["wards"].forEach((ward, index) => {
            var option = document.createElement("option");
            option.value = ward.code;
            option.text = ward.name;

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
    }
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
      //console.log($("#selection_vaccinePackage").val());
    }
  });
});

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
              <select class="selction" id="selection_vaccinePackage" type="text" name="vaccines">
              <option value="None" selected="selected"></option>
            </select>
          `;
        $("div.vaccines").append(rowHtml);
        // var x = document.getElementById("selection_vaccinePackage");

        // var option0 = document.createElement("option");
        // option0.value = "None";
        // option0.selected = "selected";
        const danhmuc_api = "https://localhost:5001/VaccinePackage/danhmuc";
        $.ajax({
          url: danhmuc_api,
          method: "get",
          success: function (response) {
            console.log(response);
            response.forEach((danhmuc, index) => {
              var option = document.createElement("option");
              option.value = danhmuc["Id"];
              option.text = danhmuc["DanhMuc"];

              document
                .getElementById("selection_vaccinePackage")
                .append(option);
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
                      <th class="blue" scope="col">Tình trạng</th>
                      <th class="blue" scope="col">
                          Giá đặt giữ theo yêu cầu (vnđ)
                      </th>
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
            <td class="blue">${vaccine["Status"]}</td>
            <td class="blue"><input type="hidden" name='priceVaccine' id='priceVaccine' value='${
              vaccine["PreOrderPrice"]
            }'/>${vaccine["PreOrderPrice"].toLocaleString("de-DE")}</td>
          
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

$(document).on("change", "#selection_vaccinePackage", function () {
  const vaccinePackage_api =
    "https://localhost:5001/VaccinePackage/danhmuc/" +
    $("#selection_vaccinePackage").val();
  console.log("hello", vaccinePackage_api);
  $("div#Package").empty();

  let rowHtml = `
  <div id="Package">
  <spand>***Chọn Gói***</spand>
  <br>
  <table id="vaccine-price-list" class="">
  <thead>
      <tr>
          <th scope="col"></th>
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
  <input id="txtSelectedID" type="hidden" name="vaccineid" value=""/>
  <input id="txtSelectedName" type="hidden" name="vaccinename" value=""/>
</table>
</div>
`;
  $("div.vaccines").append(rowHtml);
  $.ajax({
    url: vaccinePackage_api,
    method: "get",
    success: function vaccinePackageApi(response) {
      console.log(response);
      response.forEach((vaccinePackage, index) => {
        let rowHtml = `
        <tr>
          <th scope="row"><input style='width:80px' name='checkVaccine' type='checkbox' value='${
            vaccinePackage["Id"]
          }' onclick='Check();'/></th>
          <td class="light-blue" name="pakage-name">${
            vaccinePackage["GoiVaccine"]
          }</td>
          <td class="light-blue" name="package-type-name">${
            vaccinePackage["LoaiGoi"]
          }</td>
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

$(document).ready(function () {
  let bookAppointment = function () {
    console.log("post");
    const bookAppointment_api = "https://localhost:5001/BookAppointment";
    $.ajax({
      url: bookAppointment_api,
      type: "POST",
      dataType: "json",
      contentType: "application/json",
      data: JSON.stringify({
        FullName: "Vo Van A",
        BirthDate: "1999-03-19",
        Sex: "Male",
        CusID: "1",
        City: "HCM",
        District: "TD",
        Ward: "LT",
        Street: "KVC",
        NameContact: "Pham Chau",
        ContactType: "Cha",
        PhoneNumber: "0337089915",
        VaccineType: "package",
        BookAppointmentDetail: [
          {
            Id_Item: 1,
            Name: "Vắc xin cho trẻ em / 0-9 Tháng - GÓI VẮC XIN Infanrix (0-9 tháng) - GÓI LINH ĐỘNG 1",
          },
          {
            Id_Item: 2,
            Name: "Vắc xin cho trẻ em / 0-9 Tháng - GÓI VẮC XIN Infanrix (0-9 tháng) - GÓI LINH ĐỘNG 2",
          },
        ],
        Center: "HCM",
        AppointmentDate: "2022-06-30",
      }),
      cache: false,
      success: function () {
        console.log("Success");
      },
    });
  };
  $("#button_reg_ba").click(bookAppointment);
});
