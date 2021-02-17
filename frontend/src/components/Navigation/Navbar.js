import React from 'react';
import { NavLink } from 'react-router-dom';
import { createMuiTheme, makeStyles } from '@material-ui/core/styles';
import { ThemeProvider } from '@material-ui/styles';
import Button from '@material-ui/core/Button';
import { Role } from '../../Utils/Role';
import ResponsiveMenu from 'react-responsive-navbar';
import { Box } from '@material-ui/core';
import MenuOpenIcon from '@material-ui/icons/MenuOpen';
import CloseIcon from '@material-ui/icons/Close';

const useStyles = makeStyles((theme) => ({
  largeMenu: {
    backgroundColor: '#3F51B5',
  },
  link: {
    color: 'white',
  },
  smallMenu: {
    backgroundColor: '#3F51B5',
  },
  icon: {
    marginLeft: theme.spacing(2),
    display: 'flex',
    alignContent: 'flex-end',
    fontSize: 36,
    color: 'white',
  },
}));

export default function Navbar({ currentUser, setCurrentUser }) {
  const classes = useStyles();
  const buttonFont = createMuiTheme({
    typography: {
      fontFamily: ['Montserrat', 'sans-serif'].join(','),
    },
  });
  const body = (
    <>
      <ThemeProvider theme={buttonFont}>
        <Box>
          {currentUser !== null && currentUser.roles.includes(Role.Admin) && (
            <Button component={NavLink} to="/user" className={classes.link}>
              Users
            </Button>
          )}
          {currentUser !== null && currentUser.roles.includes(Role.Moderator) && (
            <Button component={NavLink} to="/meeting" className={classes.link}>
              Meetings
            </Button>
          )}
          {currentUser !== null && (
            <Button
              component={NavLink}
              to="/mymeetings"
              className={classes.link}
            >
              My meetings
            </Button>
          )}
           {currentUser !== null && (
            <Button
              component={NavLink}
              to="/template/create"
              className={classes.link}
            >
              Create template
            </Button>
          )}
          {currentUser === null ? (
            <Button component={NavLink} to="/signin" className={classes.link}>
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
              className={classes.link}
            >
              Logout
            </Button>
          )}
        </Box>
      </ThemeProvider>
    </>
  );
  return (
    <ResponsiveMenu
      menuOpenButton={<MenuOpenIcon className={classes.icon} />}
      menuCloseButton={<CloseIcon className={classes.icon} />}
      changeMenuOn="500px"
      largeMenuClassName={classes.largeMenu}
      smallMenuClassName={classes.smallMenu}
      menu={body}
    />
    // <AppBar position="sticky" className={classes.bar}>
    //   <Toolbar className={classes.toolbar}>

    //   </Toolbar>
    // </AppBar>
  );
}
