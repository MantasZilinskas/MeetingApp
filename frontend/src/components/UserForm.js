import {
  Button,
  Container,
  makeStyles,
  MenuItem,
  TextField,
} from '@material-ui/core';
import { Formik, Form } from 'formik';
import { useSnackbar } from 'notistack';
import React from 'react';
import * as yup from 'yup';
import { api } from '../axiosInstance';
import { Role } from '../Utils/Role';
import FormContainer from './FormContainer';

const validationSchema = yup.object({
  username: yup
    .string('Enter your username')
    .required('Username is required'),
  password: yup
    .string('Enter your password')
    .min(6, 'Password should be of minimum 6 characters length')
    .required('Password is required'),
  email: yup
    .string('Enter your email')
    .email('Email is not valid')
    .required('Email is required'),
  fullname: yup
    .string('Enter your full name')
    .required('Full name is required')
    .max(20, 'Full name should be of maximum 20 characters length'),
  roles: yup.array().of(yup.string()),
});

const useStyles = makeStyles((theme) => ({
  submitButton: {
    marginTop: theme.spacing(2),
  },
}));

export default function UserForm() {
  const classes = useStyles();
  const { enqueueSnackbar } = useSnackbar();
  const onSubmit = async (values) => {
    try{
      await api.post('user/register', values);
      enqueueSnackbar('User was created succesfuly', {
        anchorOrigin: {
          vertical: 'bottom',
          horizontal: 'center',
        },
        variant: 'success',
      });
    }catch(error){
      enqueueSnackbar(error.message, {
        anchorOrigin: {
          vertical: 'bottom',
          horizontal: 'center',
        },
        variant: 'error',
      });
    }
  };
  const initialValues = {
    username: '',
    password: '',
    fullname: '',
    email: '',
    roles: [],
  };
  return (
    <FormContainer header="User details">
      <Container maxWidth="xs">
        <Formik
          validationSchema={validationSchema}
          onSubmit={onSubmit}
          initialValues={initialValues}
        >
          {({ values, handleChange, touched, errors }) => (
            <Form>
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
                value={values.password}
                onChange={handleChange}
                error={touched.password && Boolean(errors.password)}
                helperText={touched.password && errors.password}
                autoComplete="current-password"
              />
              <TextField
                fullWidth
                margin="normal"
                variant="outlined"
                id="email"
                name="email"
                label="Email"
                value={values.email}
                onChange={handleChange}
                error={touched.email && Boolean(errors.email)}
                helperText={touched.email && errors.email}
                autoComplete="email"
              />
              <TextField
                fullWidth
                margin="normal"
                variant="outlined"
                id="fullname"
                name="fullname"
                label="Full name"
                value={values.fullname}
                onChange={handleChange}
                error={touched.fullname && Boolean(errors.fullname)}
                helperText={touched.fullname && errors.fullname}
                autoComplete="fullname"
              />
              <TextField
                select
                margin="normal"
                fullWidth
                name="roles"
                id="roles"
                variant="outlined"
                label="Roles"
                SelectProps={{
                  multiple: true,
                  value: values.roles,
                  onChange: handleChange,
                }}
              >
                <MenuItem value={Role.Admin}>{Role.Admin}</MenuItem>
                <MenuItem value={Role.Moderator}>{Role.Moderator}</MenuItem>
                <MenuItem value={Role.StandardUser}>
                  {Role.StandardUser}
                </MenuItem>
              </TextField>
              <Button
                color="primary"
                variant="contained"
                fullWidth
                type="submit"
                className={classes.submitButton}
              >
                Submit
              </Button>
            </Form>
          )}
        </Formik>
      </Container>
    </FormContainer>
  );
}
