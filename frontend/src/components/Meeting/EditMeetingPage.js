import {
  Card,
  Container,
  Grid,
  makeStyles,
  Typography,
} from '@material-ui/core';
import React from 'react';

const useStyles = makeStyles((theme) => ({
  h2: { fontSize: 36 },
  root: { marginTop: theme.spacing(4), flexWrap: 'wrap' },
  side: { minWidth: '25%' },
  main: { minWidth: '50%' },
}));

export default function EditMeetingPage() {
  const classes = useStyles();
  const fakeUsers = [
    { id: '1', username: 'safas1', fullname: 'safas1' },
    { id: '2', username: 'safas2', fullname: 'safas2' },
    { id: '3', username: 'safas3', fullname: 'safas3' },
  ];
  const onChange = (values) =>{
    console.log(values);
  }
  return (
    <Container>
      <Grid container className={classes.root} spacing={2}>
        <Grid item md={3} className={classes.side}>
          <Card>
            <Typography variant="h2" className={classes.h2}>
              Users
            </Typography>
          </Card>
        </Grid>
        <Grid item lg={6} className={classes.main}>
          <Card>
            <Typography variant="h2" className={classes.h2}>
              Main
            </Typography>
          </Card>
        </Grid>
        <Grid item md={3} className={classes.side}>
          <Card>
            <Typography variant="h2" className={classes.h2}>
              To do items
            </Typography>
          </Card>
        </Grid>
      </Grid>
    </Container>
  );
}
