﻿
//BurgarMenu Control on Small screeens
//NavBar, MyVolt, and avatar 
$(function () {
    'use strict';
    $('.menu-toggle').click(function () {
        $('nav').toggleClass('active')
    })

    $('#my-volt').click(function () {
        $('.volt-down').toggleClass('show')
    });

    $('#drop-down-avatar').click(function () {
        $('.avatar-down').toggleClass('show')
    });

    $(document).on('click', function (e) {
        if (!(($(e.target).closest('.sub-menu-1').length > 0) || ($(e.target).closest('.sub-menu-2').length > 0) || ($(e.target).closest('#drop-down-avatar').length > 0) || ($(e.target).closest('#my-volt').length > 0))) {
            $('.volt-down').removeClass('show');
            $('.avatar-down').removeClass('show');
        }
    });
})

 
//Hide or show page information
$(document).ready(function () {
    $('#button-info').click(function () {
        $('article').toggleClass('display')
    });
    $(document).on('click', function (e) {
        if (!(($(e.target).closest('.page-info').length > 0) || ($(e.target).closest('#button-info').length > 0))) {
            $('article').removeClass('display');
        }
    });
})

//Private conversation
$(document).ready(function () {
    $('#priv-talk').click(function () {
        $('.members-container').toggleClass('show')
    });
    //$(document).on('click', function (e) {
    //    if (!(($(e.target).closest('.private-talk-content').length > 0) || ($(e.target).closest('#priv-talk').length > 0))) {
    //        $('.members-container').removeClass('show');
    //    }
    //});
})

//Group conversation
$(document).ready(function () {
    $('#grp-talk').click(function () {
        $('.group-members-container').toggleClass('show')
    });
    //$(document).on('click', function (e) {
    //    if (!(($(e.target).closest('.group-talk-content').length > 0) || ($(e.target).closest('#grp-talk').length > 0))) {
    //        $('.group-members-container').removeClass('show');
    //    }
    //});
})

// close modal on click outside at anywhere
$(document).on('click', function (e) {
    if (!(($(e.target).closest("#modalBox").length > 0) || ($(e.target).closest("#modal-btn").length > 0))) {
        $("#modalBox").hide();
    }
});




//Posting adding show/hide
function showRecuiterDiv() {
    var x = document.getElementById("recuiterPosting");
    if (x.style.display === "block") {
        x.style.display = "none";
    } else {
        x.style.display = "block";
    }
}



//Add a new chat with chat users
function add_chatUser() {
    var chatuserInput = document.getElementById("chatUser-input").value;
    var userList = document.getElementById("user-list");
    var hiddenChatUserInput = userList.querySelector('option[value="' + chatuserInput + '"]').getAttribute("data-id");
    document.getElementById("userChat-hidden-holder").innerHTML += "<input type='hidden' name='selectedChatUsersInitalCreate' value='" + hiddenChatUserInput + "'>";
    document.getElementById("userChat-display").innerHTML += '<h6>' + chatuserInput + '</h6>';
    document.getElementById("chatUser-input").value = '';

}

function add_fields() {
    var skillInputLabel = document.getElementById("skillInputLabel").value;
    var userSkills = document.getElementById("userSkills");
    var hiddenSkillInput = userSkills.querySelector('option[value="' + skillInputLabel + '"]').getAttribute("data-id");
    document.getElementById("linkDemo").innerHTML += "<input type='hidden' name='skills' value='" + hiddenSkillInput + "'>";
    document.getElementById("enteredSkills").innerHTML += '<h6>' + skillInputLabel + '</h6>';
    document.getElementById("skillInputLabel").value = '';
}

//adding new chat users to existing chat
function add_chatUser_append() {
    var chatuserInput = document.getElementById("chatUser-appended-input").value;
    var userList = document.getElementById("user-list");
    var hiddenAppendingInput = userList.querySelector('option[value="' + chatuserInput + '"]').getAttribute("data-id");
    document.getElementById("userChat-append-hidden-holder").innerHTML += "<input type='hidden' name='selectedAppendingChatUsers' value='" + hiddenAppendingInput + "'>";
    document.getElementById("userChat-append-display").innerHTML += '<h6>' + chatuserInput + '</h6>';
    document.getElementById("chatUser-appended-input").value = '';

}

//JavaScript to control the Scroll-To-Top Button
//Get the button:
mybutton = document.getElementById("myBtn");

// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () {
    scrollFunction()
};

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