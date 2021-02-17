import { Container, makeStyles } from '@material-ui/core';
import React from 'react';
import MyEditor from '../Meeting/MyEditor';

const useStyles = makeStyles((theme) => ({
    center: {
      position: 'fixed',
      top: '50%',
      left: '50%',
    },
    container: {
        marginTop: theme.spacing(2),
    },
  }));

export default function CreateTemplate(params) {
    const classes = useStyles();
  return (
    <Container className={classes.container}>
      <MyEditor />
    </Container>
  );
}
