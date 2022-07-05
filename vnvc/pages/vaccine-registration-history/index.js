$(function () {
  console.log("hello world");
  const vaccine_registration_history_api = "https://localhost:5001/VaccinationRegistrationHistory";

    $.ajax({
        url: vaccine_registration_history_api,
        method: "get",
        success: function (response) {
            console.log(response);
            vaccineRegistrationHistoryTable(response);
        },
        error: function (errors) {
            console.log(errors);
        },
    });

    function vaccineRegistrationHistoryTable(historys) {
      historys.forEach((history, index) => {
        console.log(history);
            let rowHtml = `

          <tr>
            <th scope="row">${index + 1}</th>
            <td>${history.nguoiTiem_HoTen}</td>
            <td>${history.nguoiTiem_NgaySinh}</td>
            <td>${history.nguoiTiem_GioiTinh}</td>
            <td>${history.nguoiTiem_SDT}</td>
            <td>${history.nguoiTiem_DiaChi_SoNha_Duong}, ${history.nguoiTiem_DiaChi_Phuong}, ${history.nguoiTiem_DiaChi_Quan}, ${history.nguoiTiem_DiaChi_Tinh}</td>
            <td>${history.thongTinTiem_LoaiVC}</td>
            <td>${history.thongTinTiem_TenVC}</td>
            <td>${history.thongTinTiem_TrungTamTiem}</td>
            <td>${history.thongTinTiem_NgayDuDinhTiem}</td>
          </tr>
          `;
            $("table#vaccine-registration-history tbody").append(rowHtml);
        });
    }
});
