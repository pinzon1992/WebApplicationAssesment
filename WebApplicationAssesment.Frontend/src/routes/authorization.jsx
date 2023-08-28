import { useState } from "react";

const Authorization = () => {
    const [username, setUsername] = useState("")
    const [password, setPassword] = useState("")

    const handleUsernameChange = (e) =>{
        setUsername(e.target.value)
    }

    const handlePasswordChange = (e) =>{
        setPassword(e.target.value)
    }

    const handleAuthClick = async () =>{
        let user = {
            username: username,
            password: password
        }
        let authUrl = "https://localhost:7053/api/account/GetToken"
        const response = await fetch(authUrl, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
              },
            body: JSON.stringify(user), 
            credentials: "include"
        });
        const data = await response.json();
        if(data.code == 200){
            alert("Authorized");
        }else{
            alert("Not authorized");
        }
    }
    
    const handleLogOut = async () =>{
        let authUrl = "https://localhost:7053/api/account/LogOut"
        const response = await fetch(authUrl, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
              },
            body: JSON.stringify({}), 
            credentials: "include"
        });
        const data = await response.json();
        if(data.code == 200){
            alert("Successull log out");
        }else{
            alert("Error ocurred");
        }
    }

    return (
        <>
            <h1>Authorization</h1>
            <input type="text" placeholder="Username" value={username} onChange={handleUsernameChange}></input>
            <input type="password" placeholder="Password" value={password} onChange={handlePasswordChange}></input>
            <button type="button" onClick={handleAuthClick}>Authorize</button>
            <hr />
            <button type="button" onClick={handleLogOut}>LogOut</button>
        </>
    );
}

export {Authorization}