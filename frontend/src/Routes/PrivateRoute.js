import React from 'react';
import { Route, Redirect } from 'react-router-dom';
import { currentUserValue } from '../Utils/authenticationService';

export const PrivateRoute = ({ component: Component, roles, ...rest }) => (
    <Route {...rest} render={props => {
        const currentUser = currentUserValue();
        if (!currentUser) {
            // not logged in so redirect to login page with the return url
            return <Redirect to={{ pathname: '/signin', state: { from: props.location } }} />
        }
        // check if route is restricted by role
        if (roles && currentUser.roles && !containsRole(roles, currentUser.roles)) {
            // role not authorised so redirect to home page
            return <Redirect to={{ pathname: '/'}} />
        }
        console.log("YOOYOYOYOYOYOOYOYOYOYOY");
        // authorised so return component
        return <Component {...props} />
    }} />
)
const containsRole = (roles, userRoles) =>{
    let contains = false;
    roles.forEach(role => {
        if(userRoles.includes(role)){
            contains = true;
        }
    });
    return contains;
};