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
    // Object.entries(vaccines).forEach(function (key, vaccine) {
    //     if (selectedVaccines.includes(vaccine)) return;
    //     vaccineOptions += `<option value="${vaccine.id}" price="${vaccine.retailPrice}">Vắc xin phòng ${vaccine.protectAgainst}: ${vaccine.name}</option>`;
    // });
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
        $template.find(".vaccine-price").first().text(vaccine.retailPrice);
        $template
            .find(".vaccine-protect-against")
            .first()
            .text(vaccine.protectAgainst);
        $(".selected-vaccine-list").append($template);
    });
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
    console.log($(`input[name=gender]:checked`).length);
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
        formData[id] = $(`select#${id} option:selected`).val();
    }
    if (selectedVaccines.length == 0) {
        return false;
    }
    formData["vaccines"] = selectedVaccines;
    return formData;
}

// Data functions
function getVaccineDict(response) {
    let vaccineById = {};
    response.forEach(function (vaccine, index) {
        vaccineById[vaccine.id] = new Vaccine(
            vaccine.id,
            vaccine.name,
            vaccine.retailPrice,
            vaccine.protectAgainst
        );
    });
    return vaccineById;
}

// MAIN FUNCTION
$(function () {
    console.log("hello world");

    // PROVINCE APIs
    const PROVINCE_API = "https://provinces.open-api.vn/api/";
    const DISTRICT_API = function (provinceCode) {
        return `https://provinces.open-api.vn/api/p/${provinceCode}/?depth=2`;
    };
    const WARD_API = function (districtCode) {
        return `https://provinces.open-api.vn/api/d/${districtCode}/?depth=2`;
    };
    const VACCINE_API = "https://localhost:5001/vaccinepricelist";

    let vaccines = {};
    // User data
    let currentCustomerIndex = 0;
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
                    console.log(response);
                    displayProvinces(response);
                },
                error: function (errors) {
                    console.log(errors);
                },
            });
        }
    });

    $("body").on("change", "select#province", function (e) {
        console.log($(this).find("option:selected"));
        let provinceCode = $(this).find("option:selected").first().val();
        if (provinceCode == "") return;
        resetDistricts();
        if ($("select#district option").length < 3) {
            $.ajax({
                url: DISTRICT_API(provinceCode),
                method: "get",
                success: function (response) {
                    console.log(response);
                    displayDistricts(response);
                },
                error: function (errors) {
                    console.log(errors);
                },
            });
        }
    });

    $("body").on("change", "select#district", function (e) {
        console.log($(this).find("option:selected"));
        let districtCode = $(this).find("option:selected").first().val();
        if (districtCode == "") return;
        resetWards();
        if ($("select#ward option").length < 3) {
            $.ajax({
                url: WARD_API(districtCode),
                method: "get",
                success: function (response) {
                    console.log(response);
                    displayWards(response);
                },
                error: function (errors) {
                    console.log(errors);
                },
            });
        }
    });

    // TEST
    $("body").on("click", "button#test", function (e) {
        let formData = validateForm(selectedVaccines[currentCustomerIndex]);
        if (formData == false) {
            alert("Vui lòng điền đầy đủ thông tin");
        }
        console.log(formData);
    });

    $("body").on("click", "select#vaccines-vaccine-packs", function (e) {
        if ($("select#vaccines-vaccine-packs option").length < 2) {
            $.ajax({
                url: VACCINE_API,
                method: "get",
                success: function (response) {
                    console.log(response);
                    // displayVaccines(response);
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
});
