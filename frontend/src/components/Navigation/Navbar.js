import React from 'react';
import { NavLink } from 'react-router-dom';
import { makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Button from '@material-ui/core/Button';
//import MenuItem from '@material-ui/core/MenuItem';
import Menu from '@material-ui/core/Menu';
import { CardMedia } from '@material-ui/core';
import PropTypes from 'prop-types';

const useStyles = makeStyles(() => ({
}));

const Navbar = () => {
  const classes = useStyles();

  return (
    <AppBar position="sticky" className={classes.bar}>
      <Toolbar className={classes.toolbar}>
        <Button
          component={NavLink}
          to="/signup"
        >
          SignUp
        </Button>
        <Button
          component={NavLink}
          to="/signin"
        >
          SignIn
        </Button>
      </Toolbar>
    </AppBar>
  );
};

export default Navbar;
