import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
import Paper from '@material-ui/core/Paper';
import Typography from '@material-ui/core/Typography';
import PropTypes from 'prop-types';

const useStyles = makeStyles(() => ({
  root: {
    paddingTop: 40,
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
}));

function FormContainer(props) {
  const classes = useStyles();
  return (
    <Container maxWidth="lg" className={classes.root}>
      <Paper className={classes.header2} variant="outlined">
        <Typography variant="h2" component="h3" className={classes.header2Text}>
          {props.header}
        </Typography>
      </Paper>
      <Paper className={classes.formPaper} variant="outlined">
        {props.children}
      </Paper>
    </Container>
  );
}

FormContainer.propTypes = {
  header: PropTypes.string.isRequired,
  children: PropTypes.node.isRequired,
};

export default FormContainer;
