// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//BurgarMenu Control on Small screeens
//NavBar, MyVolt, and avatar 
$(document).ready(function () {
    $('.menu-toggle').click(function () {
        $('nav').toggleClass('active')
    })
})

$(document).ready(function () {
    $('#my-volt').click(function () {
        $('.volt-down').toggleClass('show')
    })
})
$(document).ready(function () {
    $('.avatar').click(function () {
        $('.avatar-down').toggleClass('show')
    })
})


//Hide or show page information
$(document).ready(function () {
    $('#button-info').click(function () {
        $('article').addClass('display')
    });
    $('body').click(function (){
        $('article').removeClass('display')
    })
   
})


//Posting adding show/hide
function showRecuiterDiv() {
    var x = document.getElementById("recuiterPosting");
    if (x.style.display === "block") {
        x.style.display = "none";
    } else {
        x.style.display = "block";
    }
}

function add_collab() {
    var collabInput = document.getElementById("collab-input").value;
    var userList = document.getElementById("user-list");
    var hiddenCollabInput = userList.querySelector('option[value="' + collabInput + '"]').getAttribute("data-id");
    document.getElementById("userCollab-demo").innerHTML += "<input type='hidden' name='selectedCollaborators' value='" + hiddenCollabInput +"'>";
    document.getElementById("collab-Display").innerHTML += '<h6>' + collabInput + '</h6>';
    document.getElementById("collab-input").value = '';

}


//JavaScript to control the Scroll-To-Top Button
//Get the button:
mybutton = document.getElementById("myBtn");

// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        mybutton.style.display = "block";
    } else {
        mybutton.style.display = "none";
    }
}

// When the user clicks on the button, scroll to the top of the document
function topFunction() {
    document.body.scrollTop = 0; // For Safari
    document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
}