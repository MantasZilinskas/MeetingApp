import { Button, makeStyles, TextField } from '@material-ui/core';
import { Form, Formik } from 'formik';
import React from 'react';
import FormContainer from '../FormContainer';
import MyEditor from '../Editor/MyEditor';
import * as yup from 'yup';
import { debounce } from 'lodash';

const useStyles = makeStyles((theme) => ({
  center: {
    position: 'fixed',
    top: '50%',
    left: '50%',
  },
  container: {
    alignItems: 'center',
    width: '100%',
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

export default function TemplateForm({ onSubmit, initialValues }) {
  const classes = useStyles();

  const onEditorChange = debounce((event, editor, setFieldValue) => {
    const data = editor.getData();
    setFieldValue('editorText', data);
  }, 2000);
  return (
    <FormContainer header="New template">
      <Formik
        validationSchema={validationSchema}
        onSubmit={onSubmit}
        initialValues={initialValues}
      >
        {({ values, handleChange, touched, errors, setFieldValue }) => (
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
              className={classes.textField}
            />
            <MyEditor
              className={classes.editor}
              onEditorChange={(event, editor) => {
                console.log(`Inside formik: ${values.name}`);
                onEditorChange(event, editor, setFieldValue);
              }}
              editorData={initialValues.editorText}
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
    </FormContainer>
  );
}
