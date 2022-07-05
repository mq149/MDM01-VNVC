$(function () {
  console.log("hello world");
  const vaccine_finding_center_api = "https://localhost:5001/InjectionCenter";

  $.ajax({                                      
    url: vaccine_finding_center_api,
        method: "get",
        success: function (response) {
            console.log(response);
            loadingDistrict(response);
            //findingCenterHN();
        },
        error: function (errors) {
            console.log(errors);
        },
  });
  
  const callback = (error, data) => {
    if (error) {
      console.log('>>> calling callback with error: ', error);
    }
    if (data)
    {
      console.log('>>> calling callback with data: ', data);
    }
  }

  
                                                     
  function loadingDistrict(districts){
    districts.forEach((district, index) => {
      if (district.tinh != 'HÀ NỘI' & district.tinh != 'TP.HCM')
      {
        var select = document.getElementById('comboelement')
        var opt = document.createElement('option');
        opt.value = index;
        opt.innerHTML = district.tinh;
        select.appendChild(opt);
      }
      
    });
  } 
  
});



  