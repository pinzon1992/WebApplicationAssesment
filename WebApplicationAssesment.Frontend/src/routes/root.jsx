import { Outlet } from "react-router-dom";
export default function Root() {
    return (
      <>
        <div id="sidebar">
          <div>
            <h1>User Management</h1>
          </div>
          <nav>
            <ul>
            <li>
                <a href={`/authorize`}>Authorization</a>
              </li>
              <li>
                <a href={`/roles`}>Roles</a>
              </li>
              <li>
                <a href={`/accounts`}>Accounts</a>
              </li>
              <li>
                <a href={`/users`}>Users</a>
              </li>
              <li>
                <a href={`/users/anonymous`}>Users with no authorization</a>
              </li>
            </ul>
          </nav>
        </div>
        <div id="detail">
            <Outlet />
        </div>
      </>
    );
  }