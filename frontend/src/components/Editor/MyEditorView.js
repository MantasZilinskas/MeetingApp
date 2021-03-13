import { CircularProgress, makeStyles } from '@material-ui/core';
import React, { useState, useEffect, useRef } from 'react';

const useStyles = makeStyles(() => ({
    center: {
      position: 'fixed',
      top: '50%',
      left: '50%',
    },
  }));

export default function MyEditorView({ editorData }) {
  const editorRef = useRef();
  const [editorLoaded, setEditorLoaded] = useState(false);
  const { CKEditor, ClassicEditor } = editorRef.current || {};
  const classes = useStyles();

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
        toolbar: [],
      }}
      data={editorData}
      onInit={(editor) => {}}
      onChange={(event, value) => {}}
      disabled={true}
    />
  ) : (
    <CircularProgress className={classes.center} />
  );
}
