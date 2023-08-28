import { useEffect, useState } from "react";

const Roles = () => {
    const [roles, setRoles] = useState([])
    useEffect(() => {
      refreshRolesList();
    }, [])

    const refreshRolesList = async() =>{
        let authUrl = "https://localhost:7053/api/role/roles"
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
                setRoles(data.data)
            }
        }
        
    }
    
    return (
        <>
            <h1>Roles</h1>
            <table border={1}>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                    </tr>
                    
                </thead>
                <tbody>
                    {roles.map((role) => 
                    (
                        <tr key={role.id}>
                            <td>{role.id}</td>
                             <td>{role.name}</td>
                        </tr>
                    )
                    )}
                </tbody>
            </table>
        </>
    );
}

export {Roles}