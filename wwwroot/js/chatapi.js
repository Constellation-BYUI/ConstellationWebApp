function leaveOrDeleteChat(id) {
    const DeleteChatDTO = {
        'ChatID': id
    };
    let body = JSON.stringify(DeleteChatDTO)
    let parsed = JSON.parse(body);

    var uri = "../chat/DeleteChat";

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body
    })
        //.then(response => response.json())
        .then(() => {
            alert('Removed from Chat.');
            location.reload();
        })
        .catch(error => console.error('Unable to add chat.', error));
}

function updateChat(id) {
    let hiddenSelections = document.querySelector("#userCollab-demo");
    var collabInput = document.querySelector("#collab-input").value;

    var userList = document.querySelector("#user-list");
    let selectedChatUsers = [];
    if (collabInput) {
        var collabInputDataID = userList.querySelector('option[value="' + collabInput + '"]').getAttribute("data-id");
        selectedChatUsers.push(collabInputDataID);
    }
    let ChatTitle = document.querySelector("#chatTitleInput").value;

    var i;
    for (i = 0; i < hiddenSelections.children.length; i++) {
        selectedChatUsers.push(hiddenSelections.children[i].value);
    }

    const ExistingChatDTO = {
        'ChatID': id,
        selectedChatUsers,
        ChatTitle
    };
    let body = JSON.stringify(ExistingChatDTO)
    let parsed = JSON.parse(body);

    var uri = "../chat/UpdateChat";


    fetch(uri, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body
    })
        .then(() => {
            alert('Sucessfully updated Chat!');
            location.reload();
        })
        .catch(error => console.error('Unable to add chat.', error));
}

function addMessage(id) {

    let chatMessage = document.querySelector("#newChatMessageText").value;

    const ExistingChatDTO = {
        'ChatID': id,
        chatMessage,
    };
    let body = JSON.stringify(ExistingChatDTO)
    let parsed = JSON.parse(body);

    var uri = "../chat/CreateMessage";

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body
    })
        .then(() => {
            //alert('Sent message');
            location.reload();
        })
        .catch(error => console.error('Unable to add chat.', error));
}

function addNewChat() {
    let hiddenSelections = document.querySelector("#userChat-hidden-holder");
    var collabInput = document.querySelector("#chatUser-input").value;

    var userList = document.querySelector("#user-list");
    let selectedChatUsersInitalCreate = [];
    if (collabInput) {
        var collabInputDataID = userList.querySelector('option[value="' + collabInput + '"]').getAttribute("data-id");
        selectedChatUsersInitalCreate.push(collabInputDataID);
    }
    let StartingMessage = document.querySelector("#staringMessage").value;

    var i;
    for (i = 0; i < hiddenSelections.children.length; i++) {
        selectedChatUsersInitalCreate.push(hiddenSelections.children[i].value);
    }

    const CreateChatDataDTO = {
        selectedChatUsersInitalCreate,
        StartingMessage
    };
    let body = JSON.stringify(CreateChatDataDTO)
    let parsed = JSON.parse(body);

    var uri = "../chat/CreateChatJson";


    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body
    })
        .then(() => {
            alert('Sucessfully created Chat!');
            location.reload();
        })
        .catch(error => console.error('Unable to add chat.', error));
}

//Add Collaborators or UserProjects
function add_collab() {
    var collabInput = document.querySelector("#collab-input").value;
    var userList = document.querySelector("#user-list");
    var hiddenCollabInput = userList.querySelector('option[value="' + collabInput + '"]').getAttribute("data-id");
    document.querySelector("#userCollab-demo").innerHTML += "<input type='hidden' name='selectedCollaborators' value='" + hiddenCollabInput + "'>";
    document.querySelector("#collab-Display").innerHTML += '<h6>' + collabInput + '</h6>';
    document.querySelector("#collab-input").value = '';
}


function displayChat(id) {
    document.querySelector('#displayed-chat').innerHTML = "";

    const uri = '../chat/json?id=' + id;

    fetch(uri)
        .then(function (response) {
            return response.json();
        })
        .then(function (jsonObject) {

            let userList = document.querySelector('#datalistHolder');

            const users = jsonObject['users'];
            const chats = jsonObject['chats'];
            // #region OVERARCHING SECTION CONTAINING ALL BUILD CHATS


            //CREATE EACH CHAT MESSAGE BOARD
            for (let i = 0; i < chats.length; i++) {

                // #region ITERATED SECTION CONTAINTER FOR MESSAGES & CHAT USERS
                let chatMessagesContainer = document.createElement('div');
                chatMessagesContainer.className = "msg-inbox";
                chatMessagesContainer.id = chats[i].chatID;

                let chatTitle = document.createElement('h4');
                if (chats[i].chatTitle == null) {
                    chatTitle.textContent = "New Group";
                }
                else {
                    chatTitle.textContent = chats[i].chatTitle;
                }
                document.querySelector('#displayed-chat').appendChild(chatTitle);
                // #endregion


                // #region UPDATE CHAT FORM

                // <form action="javascript:void(0);" method="post" onsubmit="leaveOrDeleteChat()">
                let updateChatDiv = document.createElement('div');
                updateChatDiv.className = "input-group";

                //<div id="collab-Display"></div>                
                let collabDisplay = document.createElement('div');
                collabDisplay.id = 'collab-Display';
                document.querySelector('#displayed-chat').appendChild(collabDisplay);

                //<input id="collab-input" class="input-group rounded border-light col-md-12" list="user-list" name="selectedCollaborators" placeholder="Select a new chat member...">
                let collabInput = document.createElement('input');
                collabInput.id = 'collab-input';
                collabInput.className = 'form-control';
                collabInput.name = "selectedCollaborators";
                collabInput.placeholder = "Select a new chat member...";
                collabInput.setAttribute('list', 'user-list');
                updateChatDiv.appendChild(collabInput);

                // <div id="userCollab-demo"></div>
                let userCollabDemo = document.createElement('div');
                userCollabDemo.id = 'userCollab-demo';
                updateChatDiv.appendChild(userCollabDemo);

                //<input class="btn btn-sm btn-outline-dark mb-1 mx-auto" type="button" id="more_collab" onclick="add_collab();" value="Add Another Chat Member" />                
                let addChatUser = document.createElement('input');
                addChatUser.type = "button";
                addChatUser.className = "btn btn-sm btn-secondary input-group-append";
                addChatUser.id = "more_collab";
                addChatUser.onclick = function () { add_collab() };
                addChatUser.value = "Add Member";
                updateChatDiv.appendChild(addChatUser);

                // #region LEAVE/DELETE BUTTON
                let removeOrLeave = document.createElement('div');
                removeOrLeave.className = 'removeOrLeave';


                let removeOrLeaveButton = document.createElement('button');
                removeOrLeaveButton.onclick = function () { leaveOrDeleteChat(id) };
                removeOrLeaveButton.textContent = "Leave Chat";
                removeOrLeaveButton.className = "btn btn-sm btn-warning";
                updateChatDiv.appendChild(removeOrLeaveButton);


                document.querySelector('#displayed-chat').appendChild(updateChatDiv);

                //Input Group for Updating Group Chat informaton
                let updateChatDivTwo = document.createElement('div');
                updateChatDivTwo.className = "input-group";

                //<input type="hidden" class="input-group rounded border-light col-md-12" name="ChatID" value="chat.ChatID">                
                let hiddenIdInput = document.createElement('input');
                hiddenIdInput.type = 'hidden';
                hiddenIdInput.class = "form-control";
                hiddenIdInput.name = 'ChatID';
                hiddenIdInput.value = id;
                updateChatDivTwo.appendChild(hiddenIdInput);

                //<input id="collab-input" class="input-group rounded border-light col-md-12" list="user-list" name="selectedCollaborators" placeholder="Select a new chat member...">
                let chatTitleInput = document.createElement('input');
                chatTitleInput.id = 'chatTitleInput';
                chatTitleInput.className = 'form-control';
                chatTitleInput.name = "chatTitleInput";
                chatTitleInput.placeholder = "Add unique Chat Title...";
                updateChatDivTwo.appendChild(chatTitleInput);

                //<input type="submit" value="Update Chat Members" class="btn btn-sm btn-outline-primary mb-1 mx-auto" />
                let updateButton = document.createElement('button');
                updateButton.onclick = function () { updateChat(id) };
                updateButton.textContent = 'Update Group';
                updateButton.className = "btn btn-sm btn-primary input-group-append";
                updateChatDivTwo.appendChild(updateButton);



                document.querySelector('#displayed-chat').appendChild(updateChatDivTwo);

                // #endregion


                //#region DISPLAY ALL CHAT USER OF BOARD
                //for (k in chats[i].chatUsers) {
                //    let ChatUserDisplay = document.createElement('div');
                //    ChatUserDisplay.className = 'chatUserDisplay';

                //    let aTag = document.createElement('a');
                //    aTag.setAttribute('href', "../../../users/details/" + chats[i].chatUsers[k].userID);
                //    aTag.innerHTML = chats[i].chatUsers[k].users[0].userName;

                //    let photoPath;
                //    if (chats[i].chatUsers[k].users.photoPath != null) {
                //        photoPath = "../../../images" + chats[i].chatUsers[k].users.photoPath;
                //    }
                //    else {
                //        photoPath = "../../../WebAssests/siteImages/avataricon.png"
                //    }

                //    let userPicture = document.createElement('img');
                //    userPicture.className = "message-images";
                //    userPicture.src = photoPath;
                //    userPicture.alt = chats[i].chatUsers[k].userName;

                //    aTag.appendChild(userPicture);
                //    ChatUserDisplay.appendChild(aTag);
                //    chatMessagesContainer.appendChild(ChatUserDisplay);
                //}
                // #endregion


                // #region DISPLAY ALL MESSAGES
                for (let j = 0; j < chats[i].chatMessages.length; j++) {


                    var displayableChats = document.createElement('div');
                    displayableChats.className = "msg-inbox";


                    document.querySelector('#displayed-chat').appendChild(displayableChats);


                    let senderID = chats[i].chatMessages[j].messages.senderID;

                    // #region MESSAGE FROM LOGGED IN USER
                    if (senderID === chats[i].currentUserId) {
                        let messageCard = document.createElement('div');
                        messageCard.className = 'outgoing-chats';

                        let fromYou = document.createElement('div');
                        fromYou.className = "outgoing-msg";

                        let outgoingChatmsg = document.createElement('div');
                        outgoingChatmsg.className = "outgoing-chats-msg";

                        let messageText = document.createElement('p');
                        messageText.textContent = chats[i].chatMessages[j].messages.messageText;
                        messageText.className = "outgoing-chats-msg p";

                        let timeSpan = document.createElement('span');
                        timeSpan.className = "time";
                        timeSpan.textContent = chats[i].chatMessages[j].messages.sentTime;


                        let userPicture = document.createElement('img');
                        userPicture.className = "outgoing-chats-img";
                        userPicture.alt = chats[i].chatMessages[j].sendersName;

                        if (chats[i].chatMessages[j].photoPath != null) {
                            userPicture.src = chats[i].chatMessages[j].photoPath;
                        }
                        else {
                            userPicture.src = "~/WebAssests/siteImages/avataricon.png"
                        }

                        outgoingChatmsg.appendChild(timeSpan);
                        outgoingChatmsg.appendChild(messageText);

                        fromYou.appendChild(outgoingChatmsg);

                        messageCard.appendChild(fromYou);
                        //messageCard.appendChild(userPicture);

                        displayableChats.appendChild(messageCard);
                    }
                    //#endregion


                    // #region MESSAGE FROM OTHERS
                    else {
                        let messageCard = document.createElement('div');
                        messageCard.className = "recieved-chats";

                        let aTag = document.createElement('a');
                        aTag.setAttribute('href', "../../../users/details/" + chats[i].chatMessages[j].messages[0].senderID);
                        aTag.className = ""

                        let userPicture = document.createElement('img');
                        userPicture.className = "recieved-chats-img";
                        userPicture.alt = chats[i].chatMessages[j].sendersName + chats[i].chatMessages[j].sendTime + 'message';

                        if (chats[i].chatMessages[j].photoPath != null) {
                            userPicture.src = "../../../images" + chats[i].chatMessages[j].photoPath;
                        }
                        else {
                            userPicture.src = "../../../WebAssests/siteImages/avataricon.png"
                        }

                        aTag.appendChild(userPicture);
                        messageCard.appendChild(aTag);

                        let fromOthers = document.createElement('div');
                        fromOthers.className = "recieved-msg";

                        let sendersName = document.createElement('div');
                        sendersName.className = "senders-name";
                        sendersName.textContent = chats[i].chatMessages[j].messages[0].senderName;

                        let outgoingChatmsg = document.createElement('div');
                        outgoingChatmsg.className = "recieved-msg-inbox";

                        let messageText = document.createElement('p');
                        messageText.textContent = chats[i].chatMessages[j].messages[0].messageText;
                        messageText.className = "recieved-msg-inbox p";

                        let timeSpan = document.createElement('span');
                        timeSpan.className = "time";
                        timeSpan.textContent = (chats[i].chatMessages[j].messages[0].sentTime);

                        outgoingChatmsg.appendChild(timeSpan);
                        outgoingChatmsg.appendChild(messageText);

                        fromOthers.appendChild(outgoingChatmsg);

                        aTag.appendChild(sendersName);

                        messageCard.appendChild(fromOthers);

                        displayableChats.appendChild(messageCard);

                    }

                    // #endregion
                    // #endregion
                }

                // #region CREATE NEW MESSAGE FORM
                let createNewMessage = document.createElement('div');
                createNewMessage.className = "input-group";

                // <div id="userCollab-demo"></div>
                let newChatMessageText = document.createElement('input');
                newChatMessageText.className = "form-control";
                newChatMessageText.id = "newChatMessageText";
                createNewMessage.appendChild(newChatMessageText);


                //<input type="submit" value="Update Chat Members" class="btn btn-sm btn-outline-primary mb-1 mx-auto" />
                let submitMessageInput = document.createElement('button');
                submitMessageInput.onclick = function () { addMessage(id) };
                submitMessageInput.textContent = "Send Message";
                submitMessageInput.className = "input-group-append btn btn-sm btn-primary";
                createNewMessage.appendChild(submitMessageInput);


                document.querySelector('#displayed-chat').appendChild(createNewMessage);
                // #endregion

                //displayableChats.appendChild(chatMessagesContainer);
                //#endregion

                //document.querySelector('#displayed-chat').appendChild(chatMessagesContainer);
            }
        });
}

//#region OnLoad
const uri = '../chat/json';
fetch(uri)
    .then(function (response) {
        return response.json();
    })
    .then(function (jsonObject) {

        const users = jsonObject['users'];

        //#region GET USER - LIST DATALIST HERE
        let datalist = document.createElement('datalist');
        datalist.id = "user-list";
        for (let i = 0; i < users.length; i++) {
            let option = document.createElement('option');
            option.value = users[i].userName;
            option.setAttribute('data-id', users[i].id);
            datalist.appendChild(option);
        };
        document.querySelector('#datalistHolder').appendChild(datalist);
        //#endregion

        // #region DISPLAY THE CHATS
        const chats = jsonObject['chats'];
        let privateChatContainer = document.createElement('div');
        privateChatContainer.id = " ";

        let groupChatContainer = document.createElement('div');
        groupChatContainer.id = "groupChatContainer";

        for (let i = 0; i < chats.length; i++) {
            //find chats with 2 people and name it the name of the person that is not 
            //currently logged in
            var count = chats[i].chatUsers.length;
            //#region PrivateCovos
            if (count <= 2) {
                let chatUserId;
                for (j in chats[i].chatUsers) {
                    if (chats[i].chatUsers[j].userID != chats[i].currentUserId) {
                        chatUserId = j;
                    }
                }

                let chatUser = chats[i].chatUsers[chatUserId];

                let privateChat = document.createElement('div');
                privateChat.className = "chat-members";

                let chatDetailsButton = document.createElement('div');
                chatDetailsButton.onclick = function () { displayChat(chats[i].chatID) };
                chatDetailsButton.className = 'chat-display';
                chatDetailsButton.Id = chats[i].chatID + 'button';

                let chatName = document.createElement('p');

                if (chatUser.users[0].firstName !== null && chatUser.users[0].lastName !== null) {
                    chatName.textContent = chatUser.users[0].firstName + " " + chatUser.users[0].lastName;
                }
                else {
                    chatName.textContent = chatUser.users[0].userName;
                }
                chatDetailsButton.appendChild(chatName);

                privateChat.appendChild(chatDetailsButton);
                privateChatContainer.appendChild(privateChat);
            }
            //#endregion
            //#region GroupConvos
            else {
                let groupChat = document.createElement('div');
                groupChat.className = "chat-members";

                let chatDetailsButton = document.createElement('div');
                chatDetailsButton.onclick = function () { displayChat(chats[i].chatID) };
                chatDetailsButton.className = 'chat-display';
                chatDetailsButton.Id = chats[i].chatID;
                let chatName = document.createElement('p');

                if (chats[i].chatTitle == null) {
                    chatName.textContent = "New Group";
                }
                else {
                    chatName.textContent = chats[i].chatTitle;
                }
                chatDetailsButton.appendChild(chatName);

                groupChat.appendChild(chatDetailsButton);
                groupChatContainer.appendChild(groupChat);

            }
        };
        document.querySelector('#privateChatsHolder').appendChild(privateChatContainer);
        document.querySelector('#groupChatsHolder').appendChild(groupChatContainer);
        //#endregion
    });
//#endregion
//#endregion

