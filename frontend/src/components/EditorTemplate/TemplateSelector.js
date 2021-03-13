import { Grid, IconButton, makeStyles, TextField } from '@material-ui/core';
import Autocomplete from '@material-ui/lab/Autocomplete';
import Skeleton from '@material-ui/lab/Skeleton';
import React, { useState } from 'react';
import { api } from '../../axiosInstance';
import { usePromiseSubscription } from '../../Utils/usePromiseSubscription';
import AddCircleIcon from '@material-ui/icons/AddCircle';

const useStyles = makeStyles((theme) => ({
  center: {
    position: 'fixed',
    top: '50%',
    left: '50%',
  },
  search: {
    minWidth: '200px',
  },
}));

export default function TemplateSelector({ setEditorData }) {
  const [isLoading, setIsLoading] = useState(true);
  const [templates, setTemplates] = useState([]);
  const [template, setTemplate] = useState({});
  const classes = useStyles();
  const fetchData = async () => {
    var result = await api.get('template');
    setTemplates(result);
    setIsLoading(false);
  };
  usePromiseSubscription(fetchData, [], []);
  if (isLoading) {
    return <Skeleton animation="wave" variant="rect" height={60} />;
  }
  return (
    <Grid container alignItems="center">
      <Grid item>
        <Autocomplete
          clearOnEscape
          className={classes.search}
          options={templates}
          onChange={(event, value) => {
            setTemplate(value);
            console.log(value);
          }}
          getOptionLabel={(option) => option.name}
          getOptionSelected={(option, value) => option.id === value.id}
          renderInput={(params) => (
            <TextField
              {...params}
              label={'Select Template'}
              variant="outlined"
            />
          )}
        />
      </Grid>
      <Grid item>
        <IconButton
          onClick={() => {
            setEditorData(template.editorText);
          }}
        >
          <AddCircleIcon />
        </IconButton>
      </Grid>
    </Grid>
  );
}
