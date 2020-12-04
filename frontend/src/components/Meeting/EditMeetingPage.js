import {
  Card,
  Container,
  Grid,
  makeStyles,
  Typography,
} from '@material-ui/core';
import React from 'react';
import UserSelect from '../UserSelect';

const useStyles = makeStyles((theme) => ({
  h2: { fontSize: 36 },
  root: { marginTop: theme.spacing(4), flexWrap: 'wrap' },
  side: { minWidth: '25%' },
  main: { minWidth: '50%' },
}));

export default function EditMeetingPage() {
  const classes = useStyles();
  return (
    <Container>
      <Grid container className={classes.root} spacing={2}>
        <Grid item md={3} className={classes.side}>
          <Card>
            <Typography variant="h2" className={classes.h2}>
              Users
            </Typography>
            <UserSelect />
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
