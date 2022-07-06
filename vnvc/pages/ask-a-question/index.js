// Đặt câu hỏi
$(document).ready(function (){
  let AskAQuestion = function(){
    if(document.getElementById("name").value ==""){
      window.alert("Vui lòng nhập Họ và Tên.")
    } else if(
      $("select#sex option:selected").text() != "Nam" &&
      $("select#sex option:selected").text() != "Nữ"
    ){
      window.alert("Vui lòng chọn giới tính.");
    }else if(document.getElementById("email").value == ""){
      window.alert("Vui lòng nhập email.");
    } else if(
      $("select#title option:selected").text() == "Chúng tôi có thể giúp gì cho bạn" 
    ){
      window.alert("Vui lòng chọn yêu cầu.");
    } else if(document.getElementById("question").value == ""){
      window.alert("Vui lòng nhập câu hỏi.");
    } else {
      const askaquestion_api = "https://localhost:5001/askaquestion"
      var arr = "";
      $.ajax({
        url: askaquestion_api,
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        data:JSON.stringify({
          FullName:document.getElementById("name").value,
          Sex: $("select#sex option:selected").text(),
          Age: document.getElementById("age").value,
          Email: document.getElementById("email").value,
          Phone: document.getElementById("phone").value,
          Address: document.getElementById("address").value,
          Title: $("select#title option:selected").text(),
          Question: document.getElementById("question").value,
          Answer: arr
        }),
        cache: false,
        success: function(){
          window.alert("Success");
        },
      });
    }
  };
  $("input#button_ask").click(AskAQuestion);
  
})
// Hiển thị câu hỏi
$(document).ready(function (){
  const askaquestion_api = "https://localhost:5001/askaquestion";
  $ajax({
    url: askaquestion_api,
    type:"get",
    dataType:"json",
    contentType: "application/json",
    success: function(response){
      askListAPI(response);
    },
    error: function(errors){
      console.log(errors);
    },
  });
  function askListAPI(askaquestions) {
    askaquestions.forEach((askaquestion, index) =>{
      let listHtml = `
      <p>${askaquestion["Question"]}</p>
      `;
      $("div.ask-box.question").append(listHtml);
    });
  }
})
