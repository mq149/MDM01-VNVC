// Classes

class Address {
    constructor(addressLine, ward, district, province) {
        this.addressLine = addressLine;
        this.ward = ward;
        this.district = district;
        this.province = province;
    }
}

// UI functions
function displayProvinces(response) {
    let $provinceSelect = $("select#province");
    let provinceOptions = "";
    response.forEach(function (province, index) {
        provinceOptions += `<option value="${province.code}">${province.name}</option>`;
    });
    $provinceSelect.append(provinceOptions);
}

function displayDistricts(response) {
    let $districtSelect = $("select#district");
    let districtOptions = "";
    if (response["districts"] == []) {
        return;
    }
    response["districts"].forEach(function (district, index) {
        districtOptions += `<option value="${district.code}">${district.name}</option>`;
    });
    $districtSelect.append(districtOptions);
}

function displayWards(response) {
    let $wardSelect = $("select#ward");
    let wardOptions = "";
    if (response["wards"] == []) {
        return;
    }
    response["wards"].forEach(function (ward, index) {
        wardOptions += `<option value="${ward.code}">${ward.name}</option>`;
    });
    $wardSelect.append(wardOptions);
}

function resetDistricts() {
    let $districtSelect = $("select#district");
    $districtSelect.html("<option value=''>Quận/Huyện</option>");
    resetWards();
}

function resetWards() {
    let $wardSelect = $("select#ward");
    $wardSelect.html("<option value=''>Phường/Xã</option>");
}

// Data function
function validateForm(customers) {
    if (customers == null) return false;
    let inputIds = [
        "fullName",
        "phoneNumber",
        "email",
        "addressLine",
        "socialId",
    ];
    let selectIds = ["province", "district", "ward"];

    let formData = {};
    if ($(`input[name=paymentMethod]:checked`).length == 0) {
        return false;
    }
    formData["paymentMethod"] = $(`input[name=paymentMethod]:checked`)
        .first()
        .val();
    for (const id of inputIds) {
        if ($(`input#${id}`).val() === "") {
            return false;
        }
        formData[id] = $(`input#${id}`).val();
    }
    for (const id of selectIds) {
        if ($(`select#${id} option:selected`).val() === "") {
            return false;
        }
        formData[id] = $(`select#${id} option:selected`).text().trim();
    }

    let registrantData = {
        information: {
            fullName: formData["fullName"],
            phoneNumber: formData["phoneNumber"],
            email: formData["email"],
            socialId: formData["socialId"],
            address: new Address(
                formData["addressLine"],
                formData["ward"],
                formData["district"],
                formData["province"]
            ),
        },
        paymentMethod: formData["paymentMethod"],
    };
    let registrationData = {
        registrant: registrantData,
        customers: customers,
    };
    return JSON.parse(JSON.stringify(registrationData));
}

function displayCustomers(customers) {
    if (customers == null) return alert("Đã có lỗi xảy ra, vui lòng thử lại");
    $(".customer-list").html("");
    var total = 0;
    for (const [index, customer] of Object.entries(customers)) {
        if (Object.keys(customer).length == 0) continue;
        let $template = $("div.customer-item-template .customer-item").clone();
        $template.attr("id", index);
        $template.find("label.name-relationship").first().text(`
            ${customer["information"]?.fullName} (${customer["relationship"]})
        `);
        let vaccineString = customer["vaccines"]
            ?.map((vaccine) => vaccine.name)
            .join(", ");
        $template
            .find("p.vaccines-info")
            .first()
            .text(`Vắc xin: ${vaccineString}`);
        $template
            .find("p.vnvc-center-info")
            .first()
            .text(`Trung tâm: ${customer["vnvcCenter"]?.name}`);
        let subtotal = customer.vaccines
            ?.map((vaccine) => vaccine.retailPrice)
            .reduce((s, a) => s + a, 0);
        total += subtotal;
        $template
            .find("label.subtotal-price")
            .first()
            .text(`${parseFloat(subtotal).toLocaleString("de-DE")} VNĐ`);
        $(".customer-list").append($template);
    }
    $("label#total-price").html(
        `${parseFloat(total).toLocaleString("de-DE")} VNĐ`
    );
}

$(function () {
    const VNVC_CUSTOMERS_LOCAL_STORAGE_KEY = "vnvc:customers";
    const VNVC_REGISTRATION_LOCAL_STORAGE_KEY = "vnvc:registrationId";

    const REGISTRATION_API = "https://localhost:5001/PrepaidVaccineProgram";

    // PROVINCE APIs
    const PROVINCE_API = "https://provinces.open-api.vn/api/";
    const DISTRICT_API = function (provinceCode) {
        return `https://provinces.open-api.vn/api/p/${provinceCode}/?depth=2`;
    };
    const WARD_API = function (districtCode) {
        return `https://provinces.open-api.vn/api/d/${districtCode}/?depth=2`;
    };

    // Load customers
    let customers = JSON.parse(
        localStorage.getItem(VNVC_CUSTOMERS_LOCAL_STORAGE_KEY)
    );
    displayCustomers(customers);

    // Event functions
    $("body").on("click", "select#province", function (e) {
        if ($(this).find("option").length < 3) {
            $.ajax({
                url: PROVINCE_API,
                method: "get",
                success: function (response) {
                    displayProvinces(response);
                },
                error: function (errors) {
                    console.log(errors);
                },
            });
        }
    });

    $("body").on("change", "select#province", function (e) {
        let provinceCode = $(this).find("option:selected").first().val();
        if (provinceCode == "") return;
        resetDistricts();
        if ($("select#district option").length < 3) {
            $.ajax({
                url: DISTRICT_API(provinceCode),
                method: "get",
                success: function (response) {
                    displayDistricts(response);
                },
                error: function (errors) {
                    console.log(errors);
                },
            });
        }
    });

    $("body").on("change", "select#district", function (e) {
        let districtCode = $(this).find("option:selected").first().val();
        if (districtCode == "") return;
        resetWards();
        if ($("select#ward option").length < 3) {
            $.ajax({
                url: WARD_API(districtCode),
                method: "get",
                success: function (response) {
                    displayWards(response);
                },
                error: function (errors) {
                    console.log(errors);
                },
            });
        }
    });

    $("body").on("click", "input[name=paymentMethod]", function (e) {
        if ($("input[name=paymentMethod]#paymentMethodRadio4").is(":checked")) {
            $(".payment-method-4-description").show();
        } else {
            $(".payment-method-4-description").hide();
        }
    });

    $("body").on("click", "button#back", function (e) {});

    $("body").on("click", "button#check-out", function (e) {
        let registrationData = validateForm(customers);
        if (registrationData == false) {
            return alert("Vui lòng nhập đầy đủ thông tin.");
        }
        $.ajax({
            headers: {
                Accept: "*/*",
                "Content-Type": "application/json",
            },
            url: REGISTRATION_API,
            method: "post",
            data: JSON.stringify(registrationData),
            dataType: "json",
            success: function (response) {
                alert("Đăng ký thành công!");
                localStorage.setItem(
                    VNVC_REGISTRATION_LOCAL_STORAGE_KEY,
                    response.Id
                );
                window.location.href = "../step-3/index.html";
            },
            error: function (errors) {
                console.log(errors);
                alert("Đã có lỗi xảy ra, vui lòng thử lại");
            },
        });
    });
});
