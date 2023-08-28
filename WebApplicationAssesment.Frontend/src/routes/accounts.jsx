import { useEffect, useState } from "react";

const Accounts = () => {
    const [accounts, setAccounts] = useState([])
    useEffect(() => {
      refreshAccountsList();
    }, [])

    const refreshAccountsList = async() =>{
        let authUrl = "https://localhost:7053/api/account/accounts"
        const response = await fetch(authUrl, {
            method: "Get",
            headers: {
                "Content-Type": "application/json",
              },
            credentials: "include"
        });        
        if (response.status == 401)
        {
            alert("Not Authorized");
        }else{
            const data = await response.json();
            if(data.code == 200){
                setAccounts(data.data)
            }
        }
    }
    
    return (
        <>
            <h1>Accounts</h1>
            <table border={1}>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Username</th>
                        <th>Role Id</th>
                    </tr>
                    
                </thead>
                <tbody>
                    {accounts.map((role) => 
                    (
                        <tr key={role.id}>
                            <td>{role.id}</td>
                             <td>{role.username}</td>
                             <td>{role.roleId}</td>
                        </tr>
                    )
                    )}
                </tbody>
            </table>
        </>
    );
}

export {Accounts}