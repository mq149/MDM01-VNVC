//Lấy các lựa chọn của khách hàng
function Check() {
  if (document.getElementById("total") != null)
    document.getElementById("total").remove();
  var s = "";
  var names = "";
  let totalPrice = 0;
  var prices = document.getElementsByName("priceVaccine");
  if ($("input.check_type.is-checked:checked").val() == "package") {
    var name01 = document.getElementsByName("packageName");
    var name02 = document.getElementsByName("packageTypeName");
    var arr = document.getElementsByName("checkVaccine");
    for (var i = 0; i < arr.length; i++) {
      if (arr[i].checked) {
        s = s + arr[i].value + ",";
        names =
          names +
          $("#selection_vaccinePackage option:selected").text() +
          " - " +
          name01[i].value +
          " - " +
          name02[i].value +
          ",";
        var p = 1 * prices[i].value;
        totalPrice += p;
      }
    }
  } else if ($("input.check_type.is-checked:checked").val() == "retail") {
    var name01 = document.getElementsByName("vaccineName");
    var arr = document.getElementsByName("checkVaccine");
    for (var i = 0; i < arr.length; i++) {
      if (arr[i].checked) {
        s = s + arr[i].value + ",";
        names = names + name01[i].value + ",";
        var p = 1 * prices[i].value;
        totalPrice += p;
      }
    }
  }

  let rowHtml = `
          <tr id='total'>
            <th scope="row" style="text-align: center;">Total Price</th>
            <td></td>
            <td class="light-blue"></td>
            <td class="light-blue"></td>
            <td class="blue"</td>
            <td class="blue"><input type="hidden" id="totalPrice" value="${totalPrice}" />${totalPrice.toLocaleString(
    "de-DE"
  )}</td>
          </tr>
          `;
  $("table#vaccine-price-list tbody").append(rowHtml);
  s = s.slice(0, -1);
  names = names.slice(0, -1);
  document.getElementById("txtSelectedID").value = s;
  document.getElementById("txtSelectedName").value = names;
  // console.log("1", names);
  // console.log("2", s);
}

//Hiển thị danh sách tỉnh thành
$(document).ready(function () {
  var x = document.getElementById("selection_city");
  const city_api = "https://provinces.open-api.vn/api/";
  $.ajax({
    url: city_api,
    method: "get",
    success: function (response) {
      // console.log(response);
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

//Hiển thị danh sách quận huyện
$(document).ready(function () {
  let dictrictList = function () {
    var x = document.getElementById("selection_dictrict");
    x.length = 0;
    if ($("#selection_city").val() != "") {
      const dictrict_api =
        "https://provinces.open-api.vn/api/p/" +
        $("#selection_city").val() +
        "/?depth=2";
      // console.log("hello", dictrict_api);
      var option0 = document.createElement("option");
      option0.value = "";
      option0.selected = "selected";
      x.append(option0);

      $.ajax({
        url: dictrict_api,
        method: "get",
        success: function dictrictListApi(response) {
          // console.log(response);
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

//Hiển thị danh sách xã phường
$(document).ready(function () {
  let wardList = function () {
    var x = document.getElementById("selection_ward");
    x.length = 0;
    if ($("#selection_dictrict").val() != "") {
      const ward_api =
        "https://provinces.open-api.vn/api/d/" +
        $("#selection_dictrict").val() +
        "/?depth=2";
      // console.log("hello", ward_api);
      var option0 = document.createElement("option");
      option0.value = "";
      option0.selected = "selected";
      x.append(option0);
      $.ajax({
        url: ward_api,
        method: "get",
        success: function wardListApi(response) {
          // console.log(response);
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

//Check lựa chọn giới tính
$(document).ready(function () {
  $("label.el-radio-button").change(function () {
    if (
      $("input.el-radio-button__orig-radio:checked").val() == "Nam" ||
      $("input.el-radio-button__orig-radio:checked").val() == "Nữ"
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

//Check khách hàng lựa chon vaccine theo gói hay riêng lẻ
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
      // console.log($("input.check_type.is-checked:checked").val());
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
            // console.log(response);
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
              <input id="txtSelectedName" type="hidden" name="txtSelectedName" value=""/>
          </table>
          `;
        $("div.vaccines").append(rowHtml);
        const vaccines_api = "https://localhost:5001/VaccinePriceList";
        $.ajax({
          url: vaccines_api,
          method: "get",
          success: function (response) {
            // console.log(response);
            response.forEach((vaccine, index) => {
              let rowHtml = `
          <tr>
            <th scope="row"><input style='width:80px' name='checkVaccine' type='checkbox' value='${
              vaccine["Id"]
            }' onclick='Check();'/></th>
            <td>${vaccine["ProtectAgainst"]}</td>
            <td class="light-blue"><input type="hidden" name='vaccineName' id='vaccineName' value='${
              vaccine["Name"]
            }' />${vaccine["Name"]}</td>
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

//Hiển thị các gói vaccine
$(document).on("change", "#selection_vaccinePackage", function () {
  const vaccinePackage_api =
    "https://localhost:5001/VaccinePackage/danhmuc/" +
    $("#selection_vaccinePackage").val();
  // console.log("hello", vaccinePackage_api);
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
          <th scope="row"><input style='width:80px' name='checkVaccine' type='checkbox' value='${
            vaccinePackage["Id"]
          }' onclick='Check();'/></th>
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

//Đăng kí tiêm chủng
$(document).ready(function () {
  let bookAppointment = function () {
    var today = new Date();
    // console.log(today);
    if (document.getElementById("name").value == "") {
      window.alert("Vui lòng nhập tên người đăng ký tiêm.");
    } else if (document.getElementById("birthday").value == "") {
      window.alert("Vui lòng nhập ngày sinh người đăng ký tiêm.");
    } else if (
      $("input.el-radio-button__orig-radio").not(":checked") &&
      $("input.el-radio-button__orig-radio.is-checked").val() != "Nam" &&
      $("input.el-radio-button__orig-radio.is-checked").val() != "Nữ"
    ) {
      window.alert("Vui lòng chọn giới tính người đăng ký tiêm.");
      //console.log($("input.el-radio-button__orig-radio.is-checked").val());
    } else if ($("#selection_city option:selected").text() == "") {
      window.alert("Vui lòng chọn tỉnh thành.");
    } else if ($("#selection_dictrict option:selected").text() == "") {
      window.alert("Vui lòng nhập chọn quận huyện.");
    } else if ($("#selection_ward option:selected").text() == "") {
      window.alert("Vui lòng chọn phường xã.");
    } else if (document.getElementById("contactname").value == "") {
      window.alert("Vui lòng nhập tên người liên hệ.");
    } else if (document.getElementById("phone").value == "") {
      window.alert("Vui lòng cung cấp số điện thoại liên hệ.");
    } else if (document.getElementById("totalPrice").value == "") {
      window.alert("Vui lòng chọn vaccine hoặc gói vaccine.");
    } else if ($("#selection_center option:selected").text() == "") {
      window.alert("Vui lòng chọn trung tâm tiêm chủng.");
    } else if (document.getElementById("bookDate").value.value == "") {
      window.alert("Vui lòng chọn ngày tiêm.");
    } else {
      const bookAppointment_api = "https://localhost:5001/BookAppointment";
      var arr1 = $("#txtSelectedID").val().split(",");
      var arr2 = $("#txtSelectedName").val().split(",");
      console.log("post", arr1, arr2);
      var arr = "[";
      for (var i = 0; i < arr1.length; i++) {
        var a = "{'Id_Item':'" + arr1[i] + "','Name':'" + arr2[i] + "'},";
        console.log("*", a, "*", arr1.length);
        arr += a;
      }
      arr += "]";
      arr = eval(arr);
      $.ajax({
        url: bookAppointment_api,
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify({
          FullName: document.getElementById("name").value,
          BirthDate: document.getElementById("birthday").value,
          Sex: $("input.el-radio-button__orig-radio.is-checked").val(),
          CusID: document.getElementById("customerid").value,
          City: $("#selection_city option:selected").text(),
          District: $("#selection_dictrict option:selected").text(),
          Ward: $("#selection_ward option:selected").text(),
          Street: document.getElementById("street").value,
          NameContact: document.getElementById("contactname").value,
          ContactType: $("#contactType option:selected").text(),
          PhoneNumber: document.getElementById("phone").value,
          VaccineType: $("input.check_type.is-checked:checked").val(),
          BookAppointmentDetail: arr,
          Center: $("#selection_center option:selected").text(),
          AppointmentDate: document.getElementById("bookDate").value,
          TotalPrice: document.getElementById("totalPrice").value,
        }),
        cache: false,
        success: function () {
          window.alert("Success");
        },
      });
    }
  };
  $("#button_reg_ba").click(bookAppointment);
});
