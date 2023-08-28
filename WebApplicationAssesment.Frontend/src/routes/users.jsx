import { useEffect, useState } from "react";

const Users = () => {
        const [users, setUsers] = useState([])
        useEffect(() => {
          refreshUsersList();
        }, [])
    
        const refreshUsersList = async() =>{
            let authUrl = "https://localhost:7053/api/user/users"
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
                    setUsers(data.data)
                }
            }
        }
        
        return (
            <>
                <h1>Users</h1>
                <table border={1}>
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Role Id</th>
                        </tr>
                        
                    </thead>
                    <tbody>
                        {users.map((role) => 
                        (
                            <tr key={role.id}>
                                <td>{role.id}</td>
                                 <td>{role.firstname}</td>
                                 <td>{role.lastname}</td>
                                 <td>{role.accountId}</td>
                            </tr>
                        )
                        )}
                    </tbody>
                </table>
            </>
        );
    }


export {Users}