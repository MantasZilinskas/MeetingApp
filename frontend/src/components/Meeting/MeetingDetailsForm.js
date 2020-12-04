import {
    Button,
    Container,
    makeStyles,
    TextField,
  } from '@material-ui/core';
  import { Formik, Form } from 'formik';
  import React from 'react';
  import * as yup from 'yup';
  import FormContainer from '../FormContainer';
  
  const validationSchema = yup.object({
    description: yup
      .string(),
    name: yup
      .string('Enter your name')
      .required('Name is required')
      .max(20, 'Name should be of maximum 20 characters length'),
    roles: yup.array().of(yup.string()),
  });
  
  const useStyles = makeStyles((theme) => ({
    submitButton: {
      marginTop: theme.spacing(2),
    },
  }));
  
  export default function MeetingDetailsForm({onSubmit, initialValues}) {
    const classes = useStyles();
    return (
      <FormContainer header="Meeting details">
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
                  id="name"
                  name="name"
                  label="Name"
                  value={values.name}
                  onChange={handleChange}
                  error={touched.name && Boolean(errors.name)}
                  helperText={touched.name && errors.name}
                  autoComplete="name"
                  autoFocus
                />
                <TextField
                  fullWidth
                  margin="normal"
                  variant="outlined"
                  id="description"
                  name="description"
                  label="Description"
                  value={values.description}
                  onChange={handleChange}
                  error={touched.description && Boolean(errors.description)}
                  helperText={touched.description && errors.description}
                  autoComplete="description"
                />
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
  