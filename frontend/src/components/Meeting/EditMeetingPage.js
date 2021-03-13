import {
  Box,
  Card,
  CircularProgress,
  Container,
  Grid,
  IconButton,
  makeStyles,
  Typography,
} from '@material-ui/core';
import EditIcon from '@material-ui/icons/Edit';
import React, { useState } from 'react';
import { NavLink, useParams } from 'react-router-dom';
import { api } from '../../axiosInstance';
import MyEditor from '../Editor/MyEditor';
import TodoItemListEdit from './TodoItem/TodoItemListEdit';
import UserSelect from './User/UserSelect';
import { debounce } from 'lodash';
import TemplateSelector from '../EditorTemplate/TemplateSelector';
import { usePromiseSubscription } from '../../Utils/usePromiseSubscription';

const useStyles = makeStyles((theme) => ({
  h2: { fontSize: 36 },
  root: { marginTop: theme.spacing(2), flexWrap: 'wrap' },
  side: { minWidth: '25%' },
  main: { minWidth: '50%' },
  center: {
    position: 'fixed',
    top: '50%',
    left: '50%',
  },
  editor: { maxHeight: '600px', overflow: 'auto' },
  italic: { fontStyle: 'italic', marginLeft: theme.spacing(1) },
  details: { margingBottom: theme.spacing(1) },
  template: { justifyContent: 'flex-end', alignItems: 'flex-end' },
}));

export default function EditMeetingPage() {
  const [meeting, setMeeting] = useState({
    id: null,
    name: '',
    description: '',
  });
  const [editorData, setEditorData] = useState('');
  const [isLoading, setLoading] = useState(false);
  const { meetingId } = useParams();

  const onEditorChange = debounce(async (event, editor) => {
    const data = editor.getData();
    const requestData = { textEditorData: data };
    await api.put(`meeting/${meetingId}/texteditor`, requestData);
  }, 5000);
  const fetchData = async () => {
    setLoading(true);
    const result = await api.get(`meeting/${meetingId}`);
    setMeeting(result);
    if (result.textEditorData) {
      setEditorData(result.textEditorData);
    }
    setLoading(false);
  };
  usePromiseSubscription(fetchData,[],[meetingId]);
  const classes = useStyles();
  if (isLoading) {
    return <CircularProgress className={classes.center} />;
  }
  return (
    <Container>
      <Grid container xs={12}>
        <Grid item xs={6}>
          <Box className={classes.details}>
            <Typography variant="h2">{meeting.name}</Typography>
            <Typography varaiant="body1">{meeting.description}</Typography>
            <IconButton component={NavLink} to={`${meetingId}/edit`}>
              <EditIcon />
              <Typography varaiant="body1" className={classes.italic}>
                Edit meeting details
              </Typography>
            </IconButton>
          </Box>
        </Grid>
        <Grid item container xs={6} className={classes.template}>
          <Grid item>
            <TemplateSelector setEditorData={setEditorData} />
          </Grid>
        </Grid>
      </Grid>

      <Grid container className={classes.root} spacing={2}>
        <Grid item md={3} className={classes.side} align="center">
          <Card>
            <Typography variant="h2" className={classes.h2}>
              Users
            </Typography>
            <UserSelect />
          </Card>
        </Grid>
        <Grid item lg={6} className={classes.main}>
          <Card>
            <MyEditor editorData={editorData} onEditorChange={onEditorChange} />
          </Card>
        </Grid>
        <Grid item md={3} className={classes.side} align="center">
          <Card>
            <Typography variant="h2" className={classes.h2}>
              To do items
            </Typography>
            <TodoItemListEdit />
          </Card>
        </Grid>
      </Grid>
    </Container>
  );
}
