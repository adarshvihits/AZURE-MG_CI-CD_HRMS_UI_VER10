    //let suggestions = [
    //    "Channel",
    //    "CodingLab",
    //    "CodingNepal",
    //    "YouTube",
    //    "YouTuber",
    //    "YouTube Channel",
    //    "Blogger",
    //    "Bollywood",
    //    "Vlogger",
    //    "Vechiles",
    //    "Facebook",
    //    "Freelancer",
    //    "Facebook Page",
    //    "Designer",
    //    "Developer",
    //    "Web Designer",
    //    "Web Developer",
    //    "Login Form in HTML & CSS",
    //    "How to learn HTML & CSS",
    //    "How to learn JavaScript",
    //    "How to became Freelancer",
    //    "How to became Web Designer",
    //    "How to start Gaming Channel",
    //    "How to start YouTube Channel",
    //    "What does HTML stands for?",
    //    "What does CSS stands for?",
//];
    function GetmenusDrop() {
        $.getJSON("/Account/GetmenusDrop", { }, function (data) {

            suggestions = data;

        });
    }

    GetmenusDrop();
    // getting all required elements
    const searchInput = document.querySelector(".searchInput");
    const input = searchInput.querySelector("input");
    const resultBox = searchInput.querySelector(".resultBox");
    const icon = searchInput.querySelector(".icon");
    let linkTag = searchInput.querySelector("a");
    let webLink;
    var currentFocus = -1;
    var sortedData = [];

    // if user press any key and release
    input.onkeyup = (e) => {
        let userData = e.target.value; //user enetered data
    let emptyArray = [];
    if (userData) {
        emptyArray = suggestions.filter((data) => {
            //filtering array value and user characters to lowercase and return only those words which are start with user enetered chars
            //return data.toLocaleLowerCase().startsWith(userData.toLocaleLowerCase());
            return data.toLocaleLowerCase().includes(userData.toLocaleLowerCase());
        });
        sortedData = emptyArray;
            emptyArray = emptyArray.map((data) => {
                // passing return data inside li tag
                data = highlight(userData,data);
                return data = '<li>' + data + '</li>';
            });
    searchInput.classList.add("active"); //show autocomplete box
    showSuggestions(emptyArray);
    let allList = resultBox.querySelectorAll("li");
    for (let i = 0; i < allList.length; i++) {
        //adding onclick attribute in all li tag
        allList[i].setAttribute("onclick", "select(this)");
            }
        } else {
        searchInput.classList.remove("active"); //hide autocomplete box
        }




        var x = resultBox.querySelectorAll("li");
        if (e.keyCode == 40) {
            /*If the arrow DOWN key is pressed,
            increase the currentFocus variable:*/
            currentFocus++;
            /*and and make the current item more visible:*/
            addActive(x);
            if (currentFocus > 16) {
                x.scrollTop = 50;
            }
        } else if (e.keyCode == 38) { //up
            /*If the arrow UP key is pressed,
            decrease the currentFocus variable:*/
            currentFocus--;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 13) {
            /*If the ENTER key is pressed, prevent the form from being submitted,*/
            if (currentFocus > -1) {
                /*and simulate a click on the "active" item:*/
                //var place = x[currentFocus].getElementsByTagName("input")[1].value;
                //if (x) window.location = place;

                $('#msd').val(sortedData[currentFocus]);                
                menuSearch();
            }
        }


    }
function addActive(x) {    
    if (!x) return false;
    /*start by removing the "active" class on all items:*/
     removeActive(x);
    if (currentFocus >= x.length) currentFocus = 0;
    if (currentFocus < 0) currentFocus = (x.length - 1);
    
    x[currentFocus].classList.add("highlight");
    //x[currentFocus].classList.add("active");    

    /* ******** Show item ************* */
    if (x[currentFocus].offsetTop < x[currentFocus].parentNode.scrollTop) {
        // Hidden on top, move scroll to show item
        // Just to the top of item
        x[currentFocus].parentNode.scrollTop = x[currentFocus].offsetTop;
    } else if (x[currentFocus].offsetTop > (x[currentFocus].parentNode.scrollTop + x[currentFocus].parentNode.clientHeight) - x[currentFocus].clientHeight) {
        // Hidden on bottom, move scroll to top of item + item height
        x[currentFocus].parentNode.scrollTop = x[currentFocus].offsetTop - (x[currentFocus].parentNode.clientHeight - x[currentFocus].clientHeight);
    }
    /* ******** Show item ************* */

}
function removeActive(x) {
    /*a function to remove the "highlight" class from all items:*/
    for (var i = 0; i < x.length; i++) {
        x[i].classList.remove("highlight");
        //x[i].classList.remove("active");
    }
}
function select(element) {
    debugger;
        let selectData = element.textContent;    
    input.value = selectData;
    //    icon.onclick = () => {
    //    webLink = `https://www.google.com/search?q=${selectData}`;
    //linkTag.setAttribute("href", webLink);
    //linkTag.click();
    //    }
    searchInput.classList.remove("active");
    if ($('input[name="radio_choices"]:checked').val() == 'Employee') {
        employeeSearch();
    }
    else {
        menuSearch(selectData);
    }
    }

    function showSuggestions(list) {
        let listData;
    if (!list.length) {
        userValue = input.value;
    //listData = '<li>' + userValue + '</li>';
    listData = '<li>No menu found</li>';
        } else {
        listData = list.join('');
        }
    resultBox.innerHTML = listData;
    }


function menuSearch() {

        //var url = '/Account/RedirectToMenu?MenuName='+$(menuName).val();
        //window.location.href = url;
        menuName = $('#msd').val();        
    $.ajax({

        type: 'GET',
        url: '/Account/RedirectToMenu',

    data: {MenuName: menuName},
    dataType: 'json',
    success: function (result) {                
    if (result.controller != null) {

        window.location.href = '/' + result.area + '/' + result.controller + '/' + result.action;
                }                                
            },
    error: function (xhr, textStatus, errorThrown) {
        document.body.id = "kt_body";
        $('body').html(xhr.responseText);
    }
        });
    }

function employeeSearch() {
    var url = '/Account/RedirectToEmployeeProfile?EmployeeName=' + $('#msd').val();
        window.location.href = url;
}

function highlight(text,data) {
    //var inputText = document.getElementById("inputText");
    var innerHTML = data;
    var index = innerHTML.toLocaleLowerCase().indexOf(text.toLocaleLowerCase());
    if (index >= 0) {
        innerHTML = innerHTML.substring(0, index) + "<span class='highlight'>" + innerHTML.substring(index, index + text.length) + "</span>" + innerHTML.substring(index + text.length);
        //inputText.innerHTML = innerHTML;
        return innerHTML;
    }
}
//function enterKeyPressed(event) {
//    if (event.keyCode == 13) {
//        menuSearch($('#msd').val());
        
//    } 
//}
function GetEmployeesDropdown() {

    $.getJSON("/Account/GetEmployeesDropdown", {}, function (data) {

        suggestions = data;

    });
}
function menuOrEmployee(key) {
    if (key == 'M') {
        $('#msd').attr('placeholder', 'Search Menu');
        GetmenusDrop();
    }
    else {
        GetEmployeesDropdown();
        $('#msd').attr('placeholder', 'Search Employee')
    }
}