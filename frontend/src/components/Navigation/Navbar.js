import React from 'react';
import { NavLink } from 'react-router-dom';
import { makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Button from '@material-ui/core/Button';
import { Role } from '../../Utils/Role';

const useStyles = makeStyles(() => ({}));

export default function Navbar({ currentUser, setCurrentUser }) {
  console.log(currentUser);
  const classes = useStyles();
  return (
    <AppBar position="sticky" className={classes.bar}>
      <Toolbar className={classes.toolbar}>
        {currentUser !== null && currentUser.roles.includes(Role.Admin) && (
          <Button component={NavLink} to="/user">
            Users
          </Button>
        )}
        {currentUser !== null && currentUser.roles.includes(Role.Moderator) && (
          <Button component={NavLink} to="/meeting">
            Meetings
          </Button>
        )}
        {currentUser !== null && (
          <Button component={NavLink} to="/mymeetings">
            My meetings
          </Button>
        )}

        {currentUser === null ? (
          <Button component={NavLink} to="/signin">
            Sign in
          </Button>
        ) : (
          <Button
            onClick={() => {
              localStorage.removeItem('currentUser');
              setCurrentUser(null);
            }}
            component={NavLink}
            to="/signin"
          >
            Logout
          </Button>
        )}
      </Toolbar>
    </AppBar>
  );
}
