import React from 'react'
import ReactDOM from 'react-dom/client'
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import Root from './routes/root';
import "./index.css"
import ErrorPage from './components/error-page';
import { Roles } from './routes/roles.jsx';
import { Accounts } from './routes/accounts.jsx';
import { Users } from './routes/users.jsx';
import { Authorization } from './routes/authorization';
import { UsersAnonymous } from './routes/users-anonymous';

const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "authorize/",
        element: <Authorization />,
      },
      {
        path: "roles/",
        element: <Roles />,
      },
      {
        path: "accounts/",
        element: <Accounts />,
      },
      {
        path: "users/",
        element: <Users />,
      },
      {
        path: "users/anonymous",
        element: <UsersAnonymous />,
      },
    ],
  },
  
]);
ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>,
)
