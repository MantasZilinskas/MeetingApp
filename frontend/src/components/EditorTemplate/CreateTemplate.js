import { Button, Grid, makeStyles, TextField } from '@material-ui/core';
import { Form, Formik } from 'formik';
import React from 'react';
import FormContainer from '../FormContainer';
import MyEditor from '../Meeting/MyEditor';
import * as yup from 'yup';

const useStyles = makeStyles((theme) => ({
  center: {
    position: 'fixed',
    top: '50%',
    left: '50%',
  },
  container: {
    alignItems: "center",
    width: "100%"
  },
  editor: {
    marginTop: theme.spacing(2),
    maxWidth: '800px',
  },
  textField: {
    marginTop: theme.spacing(2),
    maxWidth: '400px',
  },
  submitButton: {
    marginTop: theme.spacing(2),
    maxWidth: '200px',
  },
}));
const validationSchema = yup.object({
  name: yup
    .string('Enter your name')
    .required('Name is required')
    .max(20, 'Name should be of maximum 20 characters length'),
});

export default function CreateTemplate(params) {
  const classes = useStyles();
  const initialValues = { name: '' };
  const onSubmit = (values) => {
    console.log(values);
  };
  return (
    <FormContainer header="New template">
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
                className={classes.textField}
              />
              <MyEditor className={classes.editor} />
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
    </FormContainer>
  );
}
