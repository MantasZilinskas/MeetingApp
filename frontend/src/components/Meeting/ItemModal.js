import React, { useEffect, useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Dialog from '@material-ui/core/Dialog';
import DialogContent from '@material-ui/core/DialogContent';
import Slide from '@material-ui/core/Slide';
import DateFnsUtils from '@date-io/date-fns';
import {
  Button,
  Container,
  Paper,
  TextField,
  Typography,
} from '@material-ui/core';
import PropTypes from 'prop-types';
import { api } from '../../axiosInstance';
import { Form, Formik } from 'formik';
import * as yup from 'yup';
import {
  KeyboardDatePicker,
  MuiPickersUtilsProvider,
} from '@material-ui/pickers';
import Autocomplete from '@material-ui/lab/Autocomplete';
import Skeleton from '@material-ui/lab/Skeleton';

const Transition = React.forwardRef((props, ref) => {
  Transition.displayName = 'Transtition';
  return <Slide direction="up" ref={ref} {...props} />;
});

const validationSchema = yup.object({
  description: yup.string(),
  name: yup
    .string('Enter your name')
    .required('Name is required'),
  roles: yup.array().of(yup.string()),
});

const useStyles = makeStyles((theme) => ({
  paper: {
    position: 'absolute',
    width: 450,
    backgroundColor: theme.palette.background.paper,
    boxShadow: theme.shadows[5],
    padding: theme.spacing(2, 4, 3),
  },
  title: {
    flexGrow: 1,
    fontSize: '200%',
  },
  root: {
    marginTop: theme.spacing(2),
  },
  formPaper: {
    padding: 20,
    paddingBottom: 36,
    borderTopLeftRadius: 0,
    borderTopRightRadius: 0,
  },
  header: {
    fontSize: 36,
  },
  header2: {
    backgroundColor: '#f1f1f1',
    marginTop: 20,
    borderBottomLeftRadius: 0,
    borderBottomRightRadius: 0,
    borderBottom: 0,
    padding: '10px 20px 10px 20px',
  },
  header2Text: {
    fontSize: 30,
  },
  submitButton: {
    marginTop: theme.spacing(2),
  },
}));

function ItemModal({ modalOpen, setModalOpen, initialValues, onSubmit}) {
  const [users, setUsers] = React.useState([]);
  const [userLoading, setUserLoading] = useState(false);
  const classes = useStyles();
  const handleClose = () => {
    setUsers([]);
    setModalOpen(false);
  };
  useEffect(() => {
    const fetchData = async () => {
      setUserLoading(true);
      const userResponse = await api.get('user');
      setUsers(userResponse);
      setUserLoading(false);
    };
    fetchData();
    return clear();
  }, [modalOpen]);
  const clear = () => {
    setUsers([]);
  };
 
  const body = (
    <>
      <MuiPickersUtilsProvider utils={DateFnsUtils}>
        <Container maxWidth="lg" className={classes.root}>
          <Paper className={classes.header2} variant="outlined">
            <Typography
              variant="h2"
              component="h3"
              className={classes.header2Text}
            >
              To do details
            </Typography>
          </Paper>
          <Paper className={classes.formPaper} variant="outlined">
            <Formik
              validationSchema={validationSchema}
              onSubmit={onSubmit}
              initialValues={initialValues}
              enableReinitialize={true}
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
                  <KeyboardDatePicker
                    margin="normal"
                    disablePast
                    id="deadline"
                    name="deadline"
                    label="Deadline"
                    format="MM/dd/yyyy"
                    value={values.deadline}
                    onChange={(value) => setFieldValue('deadline', value)}
                    error={touched.deadline && Boolean(errors.deadline)}
                    helperText={touched.deadline && errors.deadline}
                    KeyboardButtonProps={{
                      'aria-label': 'change date',
                    }}
                  />
                  {userLoading ? (
                    <Skeleton animation="wave" variant="rect" height={60} />
                  ) : (
                    <Autocomplete
                      clearOnEscape
                      name="userId"
                      options={users}
                      value={users.filter(value => value.id === values.userId)[0]}
                      onChange={(event, value) => {
                        if (value) {
                          setFieldValue('userId', value.id);
                        }
                      }}
                      getOptionLabel={(option) =>
                        option.userName + ' - ' + option.fullName
                      }
                      getOptionSelected={(option, value) =>
                        option.id === value.id
                      }
                      renderInput={(params) => (
                        <TextField {...params} label={'Select User'} />
                      )}
                    />
                  )}
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
          </Paper>
        </Container>
      </MuiPickersUtilsProvider>
    </>
  );
  return (
    <Dialog
      open={modalOpen}
      fullWidth={true}
      maxWidth="sm"
      scroll="body"
      TransitionComponent={Transition}
      keepMounted
      onClose={handleClose}
    >
      <DialogContent>{body}</DialogContent>
    </Dialog>
  );
}
ItemModal.propTypes = {
  modalOpen: PropTypes.bool.isRequired,
  setModalOpen: PropTypes.func.isRequired,
};

export default ItemModal;
