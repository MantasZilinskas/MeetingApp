import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
import Typograpgy from '@material-ui/core/Typography';
import Box from '@material-ui/core/Box';
import PropTypes from 'prop-types';

const useStyles = makeStyles(() => ({
  root: {
    paddingTop: 40,
  },
  header: {
    fontSize: 36,
    marginBottom: 20,
  },
}));

function ListPage(props) {
  const classes = useStyles();
  return (
    <Container className={classes.root}>
      <Box display="flex" justifyContent="space-between" alignItems="center">
        <Typograpgy variant="h2" className={classes.header}>
          {props.header}
        </Typograpgy>
        {props.controlButtons}
      </Box>
      {props.children}
    </Container>
  );
}

ListPage.propTypes = {
  header: PropTypes.string.isRequired,
  controlButtons: PropTypes.node.isRequired,
  // eslint-disable-next-line react/require-default-props
  children: PropTypes.node,
};

export default ListPage;
