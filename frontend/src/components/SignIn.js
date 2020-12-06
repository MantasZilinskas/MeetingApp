import React, { useState } from 'react';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import TextField from '@material-ui/core/TextField';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
import { Form, Formik } from 'formik';
import * as yup from 'yup';
import { login } from '../Utils/authenticationService';
import { Redirect } from 'react-router-dom';
import { CircularProgress } from '@material-ui/core';
import { useSnackbar } from 'notistack';

const validationSchema = yup.object({
  username: yup.string('Enter your username').required('Username is required'),
  password: yup
    .string('Enter your password')
    .min(6, 'Password should be of minimum 6 characters length')
    .required('Password is required'),
});

const useStyles = makeStyles((theme) => ({
  paper: {
    marginTop: theme.spacing(8),
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  avatar: {
    margin: theme.spacing(1),
    backgroundColor: theme.palette.secondary.main,
  },
  form: {
    width: '100%', // Fix IE 11 issue.
    marginTop: theme.spacing(1),
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
  center: {
    position: 'fixed',
    top: '50%',
    left: '50%',
  },
}));

export default function SignIn({setCurrentUser}) {
  const classes = useStyles();
  const [redirect, setRedirect] = useState(false);
  const [isLoading, setLoading] = useState(false);
  const {enqueueSnackbar} = useSnackbar();
  const initialValues = {
    username: '',
    password: '',
  };
  const onSubmit = async (values) => {
    try {
      setLoading(true);
      await login(values.username, values.password, setCurrentUser);
      setRedirect(true);
      setLoading(false);
    } catch (error) {
      setLoading(false);
      enqueueSnackbar(error.message, {
        anchorOrigin: {
          vertical: 'bottom',
          horizontal: 'center',
        },
        variant: 'error',
      });
    }
  };

  if (redirect) {
    return <Redirect to="/" />;
  }
  if (isLoading) {
    return <CircularProgress className={classes.center} />;
  }

  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <div className={classes.paper}>
        <Avatar className={classes.avatar}>
          <LockOutlinedIcon />
        </Avatar>
        <Typography component="h1" variant="h5">
          Sign in
        </Typography>
        <Formik
          validationSchema={validationSchema}
          onSubmit={onSubmit}
          initialValues={initialValues}
        >
          {({ values, handleChange, touched, errors }) => (
            <Form className={classes.form}>
              <TextField
                fullWidth
                margin="normal"
                variant="outlined"
                id="username"
                name="username"
                label="Username"
                value={values.username}
                onChange={handleChange}
                error={touched.username && Boolean(errors.username)}
                helperText={touched.username && errors.username}
                autoComplete="username"
                autoFocus
              />
              <TextField
                fullWidth
                margin="normal"
                variant="outlined"
                id="password"
                name="password"
                label="Password"
                type="password"
                value={values.password}
                onChange={handleChange}
                error={touched.password && Boolean(errors.password)}
                helperText={touched.password && errors.password}
                autoComplete="current-password"
              />
              <Button
                color="primary"
                variant="contained"
                fullWidth
                type="submit"
              >
                Sign In
              </Button>
            </Form>
          )}
        </Formik>
      </div>
    </Container>
  );
}
