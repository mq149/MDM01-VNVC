// Classes
class Customer {
    constructor(
        id,
        fullName,
        dateOfBirth,
        gender,
        phoneNumber,
        email,
        address
    ) {
        this.id = id;
        this.fullName = fullName;
        this.dateOfBirth = dateOfBirth;
        this.gender = gender;
        this.phoneNumber = phoneNumber;
        this.email = email;
        this.address = address;
    }
}

class Address {
    constructor(addressLine, ward, district, province) {
        this.addressLine = addressLine;
        this.ward = ward;
        this.district = district;
        this.province = province;
    }
}

class Vaccine {
    constructor(id, name, retailPrice, protectAgainst) {
        this.id = id;
        this.name = name;
        this.retailPrice = retailPrice;
        this.protectAgainst = protectAgainst;
    }
}

class VnvcCenter {
    constructor(id, province, name) {
        this.id = id;
        this.province = province;
        this.name = name;
    }
}

class Relationship {
    constructor(name) {
        this.name = name;
    }
}

class Registrant {
    constructor(customerId, relationship, vaccines, vaccinePacks, center) {
        this.customerId = customerId;
        this.relationship = relationship;
        this.vaccines = vaccines;
        this.vaccinePacks = vaccinePacks;
        this.center = center;
    }
}

class VaccineRegistration {
    constructor(registrant, customers) {
        this.registrant = registrant;
        this.customers = customers;
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

function displayVaccines(vaccines, selectedVaccines) {
    let $vaccinesVaccinePacksSelect = $("select#vaccines-vaccine-packs");
    $vaccinesVaccinePacksSelect.html(
        '<option value="">Vắc xin/Gói vắc xin</option>'
    );
    let vaccineOptions = "";
    for (const [key, vaccine] of Object.entries(vaccines)) {
        if (selectedVaccines.includes(vaccine)) continue;
        vaccineOptions += `<option value="${vaccine.id}" price="${vaccine.retailPrice}">Vắc xin phòng ${vaccine.protectAgainst}: ${vaccine.name}</option>`;
    }
    $vaccinesVaccinePacksSelect.append(vaccineOptions);
}

function displaySelectedVaccines(selectedVaccines) {
    $(".selected-vaccine-list").html("");
    selectedVaccines.forEach(function (vaccine) {
        let $template = $(
            "div.vaccine-item-template .selected-vaccine-item"
        ).clone();
        $template.attr("id", vaccine.id);
        $template.find(".vaccine-name").first().text(vaccine.name);
        $template
            .find(".vaccine-price")
            .first()
            .text(parseFloat(vaccine.retailPrice).toLocaleString("de-DE"));
        $template
            .find(".vaccine-protect-against")
            .first()
            .text(`Phòng bệnh: ${vaccine.protectAgainst}`);
        $(".selected-vaccine-list").append($template);
    });
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

function validateForm(selectedVaccines) {
    let inputIds = [
        "fullName",
        "dateOfBirth",
        "phoneNumber",
        "email",
        "addressLine",
    ];
    let selectIds = [
        "relationship",
        "province",
        "district",
        "ward",
        "centerProvince",
        "vnvcCenter",
    ];

    let formData = {};
    if ($(`input[name=gender]:checked`).length == 0) {
        return false;
    }
    formData["gender"] = $(`input[name=gender]:checked`).first().val();
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
    if (selectedVaccines.length == 0) {
        return false;
    }
    formData["vaccines"] = selectedVaccines;
    let customerData = {
        information: {
            fullName: formData["fullName"],
            phoneNumber: formData["phoneNumber"],
            gender: formData["gender"],
            dateOfBirth: formData["dateOfBirth"],
            email: formData["email"],
            address: new Address(
                formData["addressLine"],
                formData["ward"],
                formData["district"],
                formData["province"]
            ),
        },
        relationship: formData["relationship"],
        vaccines: formData["vaccines"].map((vaccine) => {
            return {
                id: vaccine.id,
                name: vaccine.name,
                retailPrice: vaccine.retailPrice,
            };
        }),
        vaccinePackages: [],
        vnvcCenter: new VnvcCenter(
            "",
            formData["centerProvince"],
            formData["vnvcCenter"]
        ),
    };
    return JSON.parse(JSON.stringify(customerData));
}

// Data functions
function getVaccineDict(response) {
    let vaccineById = {};
    response.forEach(function (vaccine, index) {
        vaccineById[vaccine.Id] = new Vaccine(
            vaccine.Id,
            vaccine.Name,
            vaccine.RetailPrice,
            vaccine.ProtectAgainst
        );
    });
    return vaccineById;
}

// MAIN FUNCTION
$(function () {
    const VNVC_CUSTOMERS_LOCAL_STORAGE_KEY = "vnvc:customers";
    const VNVC_REGISTRATION_LOCAL_STORAGE_KEY = "vnvc:registrationId";
    localStorage.removeItem(VNVC_CUSTOMERS_LOCAL_STORAGE_KEY);
    localStorage.removeItem(VNVC_REGISTRATION_LOCAL_STORAGE_KEY);

    // PROVINCE APIs
    const PROVINCE_API = "https://provinces.open-api.vn/api/";
    const DISTRICT_API = function (provinceCode) {
        return `https://provinces.open-api.vn/api/p/${provinceCode}/?depth=2`;
    };
    const WARD_API = function (districtCode) {
        return `https://provinces.open-api.vn/api/d/${districtCode}/?depth=2`;
    };

    // VACCINE APIS and constants
    const VACCINE_API = "https://localhost:5001/vaccinepricelist";

    const MAX_CUSTOMER = 5;

    let vaccines = {};
    // User data
    var currentCustomerIndex = 0;
    let customers = {
        0: {},
        1: {},
        2: {},
        3: {},
        4: {},
    };
    let selectedVaccines = {
        0: [],
        1: [],
        2: [],
        3: [],
        4: [],
    };

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

    $("body").on("click", "select#vaccines-vaccine-packs", function (e) {
        if ($("select#vaccines-vaccine-packs option").length < 2) {
            $.ajax({
                url: VACCINE_API,
                method: "get",
                success: function (response) {
                    vaccines = getVaccineDict(response);
                    displayVaccines(
                        vaccines,
                        selectedVaccines[currentCustomerIndex]
                    );
                },
                error: function (errors) {
                    console.log(errors);
                },
            });
        }
    });

    $("body").on("click", "button#add-vaccine", function (e) {
        let $selectedVaccine = $(
            "select#vaccines-vaccine-packs option:selected"
        );
        let selectedVaccineId = $selectedVaccine.first().val();
        if (selectedVaccineId == "") return;
        selectedVaccines[currentCustomerIndex] = [
            ...selectedVaccines[currentCustomerIndex],
            vaccines[selectedVaccineId],
        ];
        displaySelectedVaccines(selectedVaccines[currentCustomerIndex]);
        displayVaccines(vaccines, selectedVaccines[currentCustomerIndex]);
    });

    $("body").on("click", "button.remove-vaccine", function (e) {
        let vaccineId = $(this)
            .parents(".selected-vaccine-item")
            .first()
            .attr("id");
        let index = selectedVaccines[currentCustomerIndex].indexOf(
            vaccines[vaccineId]
        );
        if (index > -1) {
            selectedVaccines[currentCustomerIndex].splice(index, 1);
        }
        displaySelectedVaccines(selectedVaccines[currentCustomerIndex]);
        displayVaccines(vaccines, selectedVaccines[currentCustomerIndex]);
    });

    $("body").on("click", "button#add-customer", function (e) {
        if (currentCustomerIndex < MAX_CUSTOMER) {
            saveCurrentCustomerData();
            displayCustomers(customers);
            currentCustomerIndex += 1;
            displaySelectedVaccines(selectedVaccines[currentCustomerIndex]);
            displayVaccines(vaccines, selectedVaccines[currentCustomerIndex]);
        } else {
            alert(
                `Quý khách chỉ được đăng ký cho tối đa ${MAX_CUSTOMER} người.`
            );
        }
    });

    $("body").on("click", "button#reset-form", function (e) {
        $("form").get(0).reset();
        selectedVaccines[currentCustomerIndex] = [];
        displaySelectedVaccines(selectedVaccines[currentCustomerIndex]);
        displayVaccines(vaccines, selectedVaccines[currentCustomerIndex]);
    });

    // $("body").on("click", "button.remove-customer", function (e) {
    //     let id = $(this).parents(".customer-item").first().attr("id");
    //     console.log(id);
    // });

    // $("body").on("click", "button.edit-customer", function (e) {
    //     let id = $(this).parents(".customer-item").first().attr("id");
    //     console.log(id);
    //     console.log(customers[id]);
    // });

    $("body").on("click", "button#next-step", function (e) {
        if (Object.keys(customers[0]).length == 0) {
            return alert("Vui lòng nhập thông tin người tiêm.");
        }
        let customerArray = Object.keys(customers)
            .filter((key) => Object.keys(customers[key]).length > 0)
            .map((key) => {
                return customers[key];
            });
        $(".term-condition-container").show();
        localStorage.setItem(
            VNVC_CUSTOMERS_LOCAL_STORAGE_KEY,
            JSON.stringify(customerArray)
        );
    });

    $("body").on("click", "button#tc-back", function (e) {
        $(".term-condition-container").hide();
        localStorage.removeItem(VNVC_CUSTOMERS_LOCAL_STORAGE_KEY);
    });

    $("body").on("click", "button#tc-next", function (e) {
        window.location.href = "../step-2/index.html";
    });

    // Data functions
    function saveCurrentCustomerData() {
        // let _currentCustomerIndex = $("input#currentCustomerIndex").val() || "";
        // if (_currentCustomerIndex == "") {
        //     return alert("Đã có lỗi xảy ra.");
        // }
        // currentCustomerIndex = _currentCustomerIndex;
        $("input#currentCustomerIndex").val(currentCustomerIndex);
        let formData = validateForm(selectedVaccines[currentCustomerIndex]);
        if (formData == false) {
            return alert("Vui lòng điền đầy đủ thông tin.");
        }
        console.log(formData);
        customers[currentCustomerIndex] = formData;
        $("form").get(0).reset();
    }
});
