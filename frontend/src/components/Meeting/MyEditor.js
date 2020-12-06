import React, { useState, useEffect, useRef } from 'react';
import { debounce } from 'lodash';
import { api } from '../../axiosInstance';
import { useParams } from 'react-router-dom';
import { CircularProgress, makeStyles } from '@material-ui/core';

const useStyles = makeStyles(() => ({
  center: {
    position: 'fixed',
    top: '50%',
    left: '50%',
  },
}));

export default function MyEditor({ editorData }) {
  const editorRef = useRef();
  const [editorLoaded, setEditorLoaded] = useState(false);
  const { CKEditor, ClassicEditor } = editorRef.current || {};
  const { meetingId } = useParams();
  const classes = useStyles();
  const onChange = debounce(async (event, editor) => {
    const data = editor.getData();
    const requestData = {textEditorData: data}
    await api.put(`meeting/${meetingId}/texteditor`,requestData);
    console.log(data);
  }, 5000);

  useEffect(() => {
    const { CKEditor } = require('@ckeditor/ckeditor5-react');
    const ClassicEditor = require('@ckeditor/ckeditor5-build-classic');
    editorRef.current = {
      CKEditor: CKEditor,
      ClassicEditor: ClassicEditor,
    };
    setEditorLoaded(true);
  }, []);

  return editorLoaded ? (
    <CKEditor
      editor={ClassicEditor}
      config={{
        toolbar: [
          'heading',
          '|',
          'bold',
          'italic',
          'link',
          'bulletedList',
          'numberedList',
          'blockQuote',
          'insertTable',
          'undo',
          'redo',
        ],
      }}
      data={editorData}
      onInit={(editor) => {}}
      onChange={onChange}
    />
  ) : (
    <CircularProgress className={classes.center} />
  );
}
